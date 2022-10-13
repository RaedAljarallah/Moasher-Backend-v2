using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Interfaces;
using Moasher.Application.Features.Expenditures.Commands.CreateExpenditure;
using Moasher.Application.Features.Projects.Commands.Common;
using Moasher.Domain.Entities.InitiativeEntities;
using Moasher.Domain.Events.Projects;
using Moasher.Domain.Validators;
using Moasher.Domain.ValueObjects;

namespace Moasher.Application.Features.Projects.Commands.CreateProject;

public record CreateProjectCommand : ProjectCommandBase, IRequest<ProjectDto>;

public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, ProjectDto>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;

    public CreateProjectCommandHandler(IMoasherDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ProjectDto> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var initiative = await _context.Initiatives
            .Include(i => i.Projects.Where(p => !p.Contracted))
            .ThenInclude(p => p.Expenditures)
            .Include(i => i.Projects.Where(p => !p.Contracted))
            .ThenInclude(p => p.ExpendituresBaseline)
            .FirstOrDefaultAsync(i => i.Id == request.InitiativeId, cancellationToken);

        if (initiative is null)
        {
            throw new NotFoundException();
        }

        request.ValidateAndThrow(new ProjectDomainValidator(initiative, request.Name, request.PlannedBiddingDate,
            request.ActualBiddingDate, request.PlannedContractingDate, request.Duration, request.EstimatedAmount));

        var phaseEnum = await _context.EnumTypes
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == request.PhaseEnumId, cancellationToken);

        if (phaseEnum is null)
        {
            throw new ValidationException(nameof(request.PhaseEnumId), ProjectEnumsValidationMessages.WrongPhaseEnumId);
        }
        
        var projectExpenditures = new List<InitiativeExpenditure>();
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
            projectExpenditures.Add(mappedExpenditure);
        }

        var project = _mapper.Map<InitiativeProject>(request);
        project.PhaseEnum = phaseEnum;
        project.Initiative = initiative;
        project.Expenditures.ToList().AddRange(projectExpenditures);
        projectExpenditures.ForEach(e =>
        {
            var baseline = InitiativeExpenditureBaseline.Map(e);
            project.ExpendituresBaseline.Add(baseline);
        });
        
        project.AddDomainEvent(new ProjectCreatedEvent(project));
        initiative.Projects.Add(project);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ProjectDto>(project);
    }
}