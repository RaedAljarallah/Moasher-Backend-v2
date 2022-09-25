using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Entities;
using Moasher.Domain.Entities.InitiativeEntities;
using Moasher.Domain.Entities.KPIEntities;
using Moasher.Domain.Extensions;
using Moasher.Domain.Validators;

namespace Moasher.Application.Features.Analytics.Commands.CreateAnalytic;

public record CreateAnalyticCommand : AnalyticCommandBase, IRequest<AnalyticDto>;

public class CreateAnalyticCommandHandler : IRequestHandler<CreateAnalyticCommand, AnalyticDto>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;

    public CreateAnalyticCommandHandler(IMoasherDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<AnalyticDto> Handle(CreateAnalyticCommand request, CancellationToken cancellationToken)
    {
        if (request.InitiativeId.HasValue && request.KPIId.HasValue)
        {
            throw new ValidationException("يجب تحديد جدول مرتبط واحد فقط");
        }
        
        List<Analytic> analytics;
        Initiative? initiative = null;
        KPI? kpi = null;
        if (request.InitiativeId.HasValue)
        {
            initiative = await _context.Initiatives
                .Include(i => i.Analytics)
                .FirstOrDefaultAsync(i => i.Id == request.InitiativeId, cancellationToken);

            if (initiative is null)
            {
                throw new NotFoundException();
            }
            analytics = initiative.Analytics.ToList();
        }
        else
        {
            kpi = await _context.KPIs
                .Include(k => k.Analytics)
                .FirstOrDefaultAsync(k => k.Id == request.KPIId, cancellationToken);

            if (kpi is null)
            {
                throw new NotFoundException();
            }
            analytics = kpi.Analytics.ToList();
        }
        
        request.ValidateAndThrow(new AnalyticDomainValidator(analytics, request.AnalyzedAt));
        
        var analytic = _mapper.Map<Analytic>(request);
        if (initiative is not null)
        {
            analytic.Initiative = initiative;
            initiative.Analytics.Add(analytic);
            initiative.SetLatestAnalytics();
        }
        
        if (kpi is not null)
        {
            analytic.KPI = kpi;
            kpi.Analytics.Add(analytic);
            kpi.SetLatestAnalytics();
        }

        //_context.Analytics.Add(analytic);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<AnalyticDto>(analytic);
    }
}