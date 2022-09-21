using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Interfaces;
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

    public UpdateEntityCommandHandler(IMoasherDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
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

        var hasEvent = request.Name != entity.Name;
        
        _mapper.Map(request, entity);
        _context.Entities.Update(entity);
        if (hasEvent)
        {
            entity.AddDomainEvent(new EntityUpdatedEvent(entity));
        }
        await _context.SaveChangesAsync(cancellationToken);
        
        return _mapper.Map<EntityDto>(entity);
    }
    
}