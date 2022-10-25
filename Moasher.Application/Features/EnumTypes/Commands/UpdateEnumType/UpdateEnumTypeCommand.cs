using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Events.EnumTypes;
using Moasher.Domain.Validators;

namespace Moasher.Application.Features.EnumTypes.Commands.UpdateEnumType;

public record UpdateEnumTypeCommand : EnumTypeCommandBase, IRequest<EnumTypeDto>
{
    public Guid Id { get; set; }
}

public class UpdateEnumTypeCommandHandler : IRequestHandler<UpdateEnumTypeCommand, EnumTypeDto>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;

    public UpdateEnumTypeCommandHandler(IMoasherDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<EnumTypeDto> Handle(UpdateEnumTypeCommand request, CancellationToken cancellationToken)
    {
        var enumTypes = await _context.EnumTypes
            .AsNoTracking()
            .Where(e => e.Category.ToLower() == request.Category.ToString().ToLower())
            .ToListAsync(cancellationToken);
        var enumType = enumTypes.FirstOrDefault(e => e.Id == request.Id);
        if (enumType is null)
        {
            throw new NotFoundException();
        }
        
        request.ValidateAndThrow(new EnumTypeDomainValidator(enumTypes.Where(e => e.Id != request.Id).ToList(), request.Name, request.Category, request.IsDefault));
        var hasEvent = request.Name != enumType.Name || request.Style != enumType.Style;

        if (!request.IsDefault && enumType.IsDefault)
        {
            throw new ValidationException("يجب أن يكون هناك قيمة إفتراضية");
        }
        
        _mapper.Map(request, enumType);
        if (request.IsDefault)
        {
            enumType.LimitFrom = default!;
            enumType.LimitTo = default!;
            enumType.CanBeDeleted = false;
        }

        if (hasEvent)
        {
            enumType.AddDomainEvent(new EnumTypeUpdatedEvent(enumType));
        }
        _context.EnumTypes.Update(enumType);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<EnumTypeDto>(enumType);
    }
}