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

namespace Moasher.Application.Features.Analytics.Commands.UpdateAnalytic;

public record UpdateAnalyticCommand : AnalyticCommandBase, IRequest<AnalyticDto>
{
    public Guid Id { get; set; }
}

public class UpdateAnalyticCommandHandler : IRequestHandler<UpdateAnalyticCommand, AnalyticDto>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;

    public UpdateAnalyticCommandHandler(IMoasherDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<AnalyticDto> Handle(UpdateAnalyticCommand request, CancellationToken cancellationToken)
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
        
        var analytic = analytics.FirstOrDefault(a => a.Id == request.Id);
        if (analytic is null)
        {
            throw new NotFoundException();
        }
        
        request.ValidateAndThrow(new AnalyticDomainValidator(analytics.Where(a => a.Id != request.Id).ToList(), request.AnalyzedAt));
        
        _mapper.Map(request, analytic);

        initiative?.SetLatestAnalytics();
        kpi?.SetLatestAnalytics();
        
        _context.Analytics.Update(analytic);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<AnalyticDto>(analytic);
    }
}