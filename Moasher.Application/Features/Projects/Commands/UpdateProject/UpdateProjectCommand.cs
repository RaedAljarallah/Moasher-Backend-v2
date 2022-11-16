using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Interfaces;
using Moasher.Application.Features.Expenditures.Commands.CreateProjectExpenditure;
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
            .Include(i => i.Projects)
            .ThenInclude(p => p.ContractMilestones)
            .Include(i => i.Projects)
            .ThenInclude(p => p.Progress)
            .AsSplitQuery()
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
            request.ActualBiddingDate, request.PlannedContractingDate, request.PlannedContractEndDate,
            request.EstimatedAmount));
        
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
            
            var activeProgressItem = project.Progress.FirstOrDefault(p => !p.Completed);
            activeProgressItem?.Complete();
            
            var newProgressItem = InitiativeProjectProgress.CreateProjectProgressItem(project);
            project.Progress.Add(newProgressItem);
        }

        if (IsDifferentExpenditures(request, project))
        {
            _context.InitiativeExpenditures.RemoveRange(project.Expenditures);
            foreach (var expenditure in request.Expenditures)
            {
                if (expenditure.PlannedAmount == 0) continue;
                
                var expenditureValidator = new CreateProjectExpenditureCommandValidator();
                expenditureValidator.SetValidationArguments(request.PlannedContractingDate, request.PlannedContractEndDate);
        
                var expenditureValidationResult = expenditureValidator.Validate(expenditure);
                if (!expenditureValidationResult.IsValid)
                {
                    throw new ValidationException(expenditureValidationResult.Errors);
                }
        
                var mappedExpenditure = _mapper.Map<InitiativeExpenditure>(expenditure);
                project.Expenditures.Add(mappedExpenditure);
            }
        }

        if (IsDifferentMilestones(request, project))
        {
            _context.ContractMilestones.RemoveRange(project.ContractMilestones);
            if (request.MilestoneIds.Any())
            {
                var milestones = await _context.InitiativeMilestones
                    .Where(m => m.InitiativeId == initiative.Id)
                    .Where(m => request.MilestoneIds.Contains(m.Id))
                    .ToListAsync(cancellationToken);

                foreach (var milestone in milestones)
                {
                    project.ContractMilestones.Add(new ContractMilestone
                    {
                        Milestone = milestone,
                        Project = project
                    });
                }
            }
        }
        
        _mapper.Map(request, project);
        _context.InitiativeProjects.Update(project);
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

    private static bool IsDifferentMilestones(UpdateProjectCommand request, InitiativeProject project)
    {
        var currentMilestones = request.MilestoneIds.ToList();
        var originalMilestones = project.ContractMilestones.Select(cm => cm.MilestoneId).ToList();

        if (currentMilestones.Count != originalMilestones.Count)
        {
            return true;
        }

        if (!currentMilestones.SequenceEqual(originalMilestones))
        {
            return true;
        }

        return false;
    }
}