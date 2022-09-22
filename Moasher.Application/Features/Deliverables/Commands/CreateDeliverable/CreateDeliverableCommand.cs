using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Entities.InitiativeEntities;
using Moasher.Domain.Validators;

namespace Moasher.Application.Features.Deliverables.Commands.CreateDeliverable;

public record CreateDeliverableCommand : DeliverableCommandBase, IRequest<DeliverableDto>;

public class CreateDeliverableCommandHandler : IRequestHandler<CreateDeliverableCommand, DeliverableDto>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;

    public CreateDeliverableCommandHandler(IMoasherDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<DeliverableDto> Handle(CreateDeliverableCommand request, CancellationToken cancellationToken)
    {
        var initiative = await _context.Initiatives
            .Include(i => i.Deliverables)
            .FirstOrDefaultAsync(i => i.Id == request.InitiativeId, cancellationToken);
        
        if (initiative is null)
        {
            throw new NotFoundException();
        }
        
        request.ValidateAndThrow(new DeliverableDomainValidator(initiative, request.Name, request.PlannedFinish, request.ActualFinish));
        
        var deliverable = _mapper.Map<InitiativeDeliverable>(request);
        deliverable.Initiative = initiative;
        initiative.Deliverables.Add(deliverable);
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<DeliverableDto>(deliverable);
    }
}