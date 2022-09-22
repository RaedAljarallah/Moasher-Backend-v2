using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Validators;

namespace Moasher.Application.Features.Deliverables.Commands.UpdateDeliverable;

public record UpdateDeliverableCommand : DeliverableCommandBase, IRequest<DeliverableDto>
{
    public Guid Id { get; set; }
}

public class UpdateDeliverableCommandHandler : IRequestHandler<UpdateDeliverableCommand, DeliverableDto>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;

    public UpdateDeliverableCommandHandler(IMoasherDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<DeliverableDto> Handle(UpdateDeliverableCommand request, CancellationToken cancellationToken)
    {
        var initiative = await _context.Initiatives
            .AsNoTracking()
            .Include(i => i.Deliverables)
            .FirstOrDefaultAsync(i => i.Id == request.InitiativeId, cancellationToken);
        
        if (initiative is null)
        {
            throw new NotFoundException();
        }
        
        var deliverable = initiative.Deliverables.FirstOrDefault(d => d.Id == request.Id);
        if (deliverable is null)
        {
            throw new NotFoundException();
        }
        
        initiative.Deliverables = initiative.Deliverables.Where(d => d.Id != request.Id).ToList();
        request.ValidateAndThrow(new DeliverableDomainValidator(initiative, request.Name,
            request.PlannedFinish, request.ActualFinish));
        
        _mapper.Map(request, deliverable);
        _context.InitiativeDeliverables.Update(deliverable);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<DeliverableDto>(deliverable);
    }
}