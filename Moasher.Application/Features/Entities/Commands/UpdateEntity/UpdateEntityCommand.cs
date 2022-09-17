using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Interfaces;
using Moasher.Application.Features.Entities.BackgroundJobs;
using Moasher.Domain.Events.Entities;
using Moasher.Domain.Validators;

namespace Moasher.Application.Features.Entities.Commands.UpdateEntity;

public record UpdateEntityCommand : EntityCommandBase, IRequest<EntityDto>
{
    public Guid Id { get; set; }
}

public class UpdateEntityCommandHandler : IRequestHandler<UpdateEntityCommand, EntityDto>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;
    private readonly IBackgroundQueue _queue;
    private readonly IEntityUpdatedJob _entityUpdatedJob;

    public UpdateEntityCommandHandler(IMoasherDbContext context, IMapper mapper, IBackgroundQueue queue, IEntityUpdatedJob entityUpdatedJob)
    {
        _context = context;
        _mapper = mapper;
        _queue = queue;
        _entityUpdatedJob = entityUpdatedJob;
    }
    
    public async Task<EntityDto> Handle(UpdateEntityCommand request, CancellationToken cancellationToken)
    {
        var entities = await _context.Entities.AsNoTracking().ToListAsync(cancellationToken);
        var entity = entities.FirstOrDefault(e => e.Id == request.Id);
        if (entity is null)
        {
            throw new NotFoundException();
        }
        
        request.ValidateAndThrow(new EntityDomainValidator(entities.Where(e => e.Id != request.Id).ToList(), request.Name, request.Code));
        
        var runBackgroundJob = (request.Name != entity.Name);
        
        _mapper.Map(request, entity);
        // entity.DomainEvents.Add(new EntityUpdatedEvent(entity));
        _context.Entities.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);

        // if (runBackgroundJob)
        // {
        //     await _queue.QueueTask(ct => _entityUpdatedJob.ExecuteAsync(entity.Id, ct));
        // }
        
        return _mapper.Map<EntityDto>(entity);
    }
    
}