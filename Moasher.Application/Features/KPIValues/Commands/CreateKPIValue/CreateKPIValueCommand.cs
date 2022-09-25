using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Entities.KPIEntities;
using Moasher.Domain.Events.KPIValues;
using Moasher.Domain.Validators;

namespace Moasher.Application.Features.KPIValues.Commands.CreateKPIValue;

public record CreateKPIValueCommand : KPIValueCommandBase, IRequest<KPIValueDto>;

public class CreateKPIValueCommandHandler : IRequestHandler<CreateKPIValueCommand, KPIValueDto>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;

    public CreateKPIValueCommandHandler(IMoasherDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<KPIValueDto> Handle(CreateKPIValueCommand request, CancellationToken cancellationToken)
    {
        var kpi = await _context.KPIs
            .Include(k => k.Values)
            .FirstOrDefaultAsync(k => k.Id == request.KPIId, cancellationToken);

        if (kpi is null)
        {
            throw new NotFoundException();
        }
        
        request.ValidateAndThrow(new KPIValueDomainValidator(kpi.Values.ToList(), request.Year, request.MeasurementPeriod));
        
        var value = _mapper.Map<KPIValue>(request);
        value.KPI = kpi;
        value.AddDomainEvent(new KPIValueCreatedEvent(value));
        kpi.Values.Add(value);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<KPIValueDto>(value);
    }
}