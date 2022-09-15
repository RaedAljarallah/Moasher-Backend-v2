using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Interfaces;
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
            .Where(e => e.Category == request.Category.ToString())
            .ToListAsync(cancellationToken);
        var enumType = enumTypes.FirstOrDefault(e => e.Id == request.Id);
        if (enumType is null)
        {
            throw new NotFoundException();
        }
        
        request.ValidateAndThrow(new EnumTypeDomainValidator(enumTypes.Where(e => e.Id != request.Id).ToList(), request.Name, request.Category));
        
        _mapper.Map(request, enumType);
        enumType.Metadata = request.Metadata;
        _context.EnumTypes.Update(enumType);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<EnumTypeDto>(enumType);
    }
}