using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Events.KPIValues;
using Moasher.Domain.Validators;

namespace Moasher.Application.Features.KPIValues.Commands.UpdateKPIValue;

public record UpdateKPIValueCommand : KPIValueCommandBase, IRequest<KPIValueDto>
{
    public Guid Id { get; set; }
}

public class KPIValueCommandBaseHandler : IRequestHandler<UpdateKPIValueCommand, KPIValueDto>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;

    public KPIValueCommandBaseHandler(IMoasherDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<KPIValueDto> Handle(UpdateKPIValueCommand request, CancellationToken cancellationToken)
    {
        var values = await _context.KPIValues
            .Where(v => v.KPIId == request.KPIId)
            .ToListAsync(cancellationToken);

        var value = values.FirstOrDefault(v => v.Id == request.Id);
        if (value is null)
        {
            throw new NotFoundException();
        }

        request.ValidateAndThrow(new KPIValueDomainValidator(values.Where(v => v.Id != request.Id).ToList(),
            request.Year, request.MeasurementPeriod));

        _mapper.Map(request, value);
        value.AddDomainEvent(new KPIValueUpdatedEvent(value));
        _context.KPIValues.Update(value);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<KPIValueDto>(value);
    }
}