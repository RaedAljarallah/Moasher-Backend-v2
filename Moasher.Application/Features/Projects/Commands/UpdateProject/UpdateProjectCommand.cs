using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Interfaces;
using Moasher.Application.Features.Expenditures.Commands.CreateExpenditure;
using Moasher.Application.Features.Projects.Commands.Common;
using Moasher.Domain.Entities.InitiativeEntities;
using Moasher.Domain.Validators;

namespace Moasher.Application.Features.Projects.Commands.UpdateProject;

public record UpdateProjectCommand : ProjectCommandBase, IRequest<ProjectDto>
{
    public Guid Id { get; set; }
}

public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, ProjectDto>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;

    public UpdateProjectCommandHandler(IMoasherDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<ProjectDto> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var initiative = await _context.Initiatives
            .AsNoTracking()
            .Include(i => i.Projects)
            .ThenInclude(p => p.Expenditures)
            .FirstOrDefaultAsync(i => i.Id == request.InitiativeId, cancellationToken);
        
        if (initiative is null)
        {
            throw new NotFoundException();
        }
        
        var project = initiative.Projects.FirstOrDefault(p => p.Id == request.Id);
        if (project is null)
        {
            throw new NotFoundException();
        }
        
        initiative.Projects = initiative.Projects.Where(p => p.Id != request.Id).ToList();
        request.ValidateAndThrow(new ProjectDomainValidator(initiative, request.Name, request.PlannedBiddingDate,
            request.ActualBiddingDate, request.PlannedContractingDate, request.Duration));
        
        if (request.PhaseEnumId != project.PhaseEnumId)
        {
            var phaseEnum = await _context.EnumTypes
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == request.PhaseEnumId, cancellationToken);
            
            if (phaseEnum is null)
            {
                throw new ValidationException(nameof(request.PhaseEnumId), ProjectEnumsValidationMessages.WrongPhaseEnumId);
            }
        
            project.PhaseEnum = phaseEnum;
        }
        
        if (IsDifferentExpenditures(request, project))
        {
            _context.InitiativeExpenditures.RemoveRange(project.Expenditures);
            foreach (var expenditure in request.Expenditures)
            {
                if (expenditure.PlannedAmount == 0) continue;
                
                var expenditureValidator = new CreateExpenditureCommandValidator();
                expenditureValidator.SetValidationArguments(request.PlannedContractingDate,
                    request.PlannedContractingDate.AddMonths(request.Duration));
        
                var expenditureValidationResult = expenditureValidator.Validate(expenditure);
                if (!expenditureValidationResult.IsValid)
                {
                    throw new ValidationException(expenditureValidationResult.Errors);
                }
        
                var mappedExpenditure = _mapper.Map<InitiativeExpenditure>(expenditure);
                project.Expenditures.Add(mappedExpenditure);
            }
        }
        
        _mapper.Map(request, project);
        _context.TrackModified(project);
        await _context.SaveChangesAsync(cancellationToken);
        
        return _mapper.Map<ProjectDto>(project);
    }

    private static bool IsDifferentExpenditures(UpdateProjectCommand request, InitiativeProject project)
    {
        var currentExpenditures = request.Expenditures
            .OrderByDescending(e => e.Year)
            .ThenBy(e => e.Month)
            .Select(e => new 
            {
                e.Year,
                e.Month,
                e.PlannedAmount,
                e.ActualAmount
            }).ToList();
        
        var originalExpenditures = project.Expenditures
            .OrderByDescending(e => e.Year)
            .ThenBy(e => e.Month)
            .Select(e => new
            {
                e.Year,
                e.Month,
                e.PlannedAmount,
                e.ActualAmount
            }).ToList();
        
        if (currentExpenditures.Count != originalExpenditures.Count)
        {
            return true;
        }

        if (!currentExpenditures.SequenceEqual(originalExpenditures))
        {
            return true;
        }
        
        return false;
    }
}