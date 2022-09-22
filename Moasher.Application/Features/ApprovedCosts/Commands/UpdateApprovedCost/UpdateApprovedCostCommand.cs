using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Events.ApprovedCosts;
using Moasher.Domain.Validators;

namespace Moasher.Application.Features.ApprovedCosts.Commands.UpdateApprovedCost;

public record UpdateApprovedCostCommand : ApprovedCostCommandBase, IRequest<ApprovedCostDto>
{
    public Guid Id { get; set; }
}

public class UpdateApprovedCostCommandHandler : IRequestHandler<UpdateApprovedCostCommand, ApprovedCostDto>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;

    public UpdateApprovedCostCommandHandler(IMoasherDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<ApprovedCostDto> Handle(UpdateApprovedCostCommand request, CancellationToken cancellationToken)
    {
        var initiative = await _context.Initiatives
            .AsNoTracking()
            .Include(i => i.ApprovedCosts)
            .FirstOrDefaultAsync(i => i.Id == request.InitiativeId, cancellationToken);
        if (initiative is null)
        {
            throw new NotFoundException();
        }

        var approvedCost = initiative.ApprovedCosts.FirstOrDefault(c => c.Id == request.Id);
        if (approvedCost is null)
        {
            throw new NotFoundException();
        }
        
        initiative.ApprovedCosts = initiative.ApprovedCosts.Where(c => c.Id != request.Id).ToList();
        request.ValidateAndThrow(new ApprovedCostDomainValidator(initiative, request.ApprovalDate, request.Amount));

        _mapper.Map(request, approvedCost);
        approvedCost.AddDomainEvent(new ApprovedCostUpdatedEvent(approvedCost));
        _context.InitiativeApprovedCosts.Update(approvedCost);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ApprovedCostDto>(approvedCost);
    }
}