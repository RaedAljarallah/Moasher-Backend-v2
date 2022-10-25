using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Entities;
using Moasher.Domain.Validators;

namespace Moasher.Application.Features.EnumTypes.Commands.CreateEnumType;

public record CreateEnumTypeCommand : EnumTypeCommandBase, IRequest<EnumTypeDto>;

public class CreateEnumTypeCommandHandler : IRequestHandler<CreateEnumTypeCommand, EnumTypeDto>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;

    public CreateEnumTypeCommandHandler(IMoasherDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<EnumTypeDto> Handle(CreateEnumTypeCommand request, CancellationToken cancellationToken)
    {
        var enumTypes = await _context.EnumTypes
            .AsNoTracking()
            .Where(e => e.Category.ToLower() == request.Category.ToString().ToLower())
            .ToListAsync(cancellationToken);
        
        request.ValidateAndThrow(new EnumTypeDomainValidator(enumTypes, request.Name, request.Category, request.IsDefault));
        var enumType = _mapper.Map<EnumType>(request);
        if (request.IsDefault)
        {
            enumType.LimitFrom = default!;
            enumType.LimitTo = default!;
            enumType.CanBeDeleted = false;
        }
        _context.EnumTypes.Add(enumType);
        await _context.SaveChangesAsync(cancellationToken);
        
        return _mapper.Map<EnumTypeDto>(enumType);
    }
}