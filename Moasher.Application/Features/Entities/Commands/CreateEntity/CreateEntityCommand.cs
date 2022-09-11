using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Entities;
using Moasher.Domain.Validators;

namespace Moasher.Application.Features.Entities.Commands.CreateEntity;

public record CreateEntityCommand : EntityCommandBase, IRequest<EntityDto>
{
}

public class CreateEntityCommandHandler : IRequestHandler<CreateEntityCommand, EntityDto>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;

    public CreateEntityCommandHandler(IMoasherDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<EntityDto> Handle(CreateEntityCommand request, CancellationToken cancellationToken)
    {
        var entities = await _context.Entities.AsNoTracking().ToListAsync(cancellationToken);
        request.ValidateAndThrow(new EntityDomainValidator(entities, request.Name, request.Code));
        var entity = _mapper.Map<Entity>(request);
        _context.Entities.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<EntityDto>(entity);
    }
}