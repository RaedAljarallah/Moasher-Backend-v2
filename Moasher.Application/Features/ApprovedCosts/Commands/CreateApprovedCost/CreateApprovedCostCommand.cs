using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Entities.InitiativeEntities;
using Moasher.Domain.Events.ApprovedCosts;
using Moasher.Domain.Validators;

namespace Moasher.Application.Features.ApprovedCosts.Commands.CreateApprovedCost;

public record CreateApprovedCostCommand : ApprovedCostCommandBase, IRequest<ApprovedCostDto>;

public class CreateApprovedCostCommandHandler : IRequestHandler<CreateApprovedCostCommand, ApprovedCostDto>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;

    public CreateApprovedCostCommandHandler(IMoasherDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<ApprovedCostDto> Handle(CreateApprovedCostCommand request, CancellationToken cancellationToken)
    {
        var initiative = await _context.Initiatives
            .Include(i => i.ApprovedCosts)
            .FirstOrDefaultAsync(i => i.Id == request.InitiativeId, cancellationToken);
        
        if (initiative is null)
        {
            throw new NotFoundException();
        }
        
        request.ValidateAndThrow(new ApprovedCostDomainValidator(initiative, request.ApprovalDate, request.Amount));
        var approvedCost = _mapper.Map<InitiativeApprovedCost>(request);
        approvedCost.Initiative = initiative;
        approvedCost.AddDomainEvent(new ApprovedCostCreatedEvent(approvedCost));
        initiative.ApprovedCosts.Add(approvedCost);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ApprovedCostDto>(approvedCost);
    }
}