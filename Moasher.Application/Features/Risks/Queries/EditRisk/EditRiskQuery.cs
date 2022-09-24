using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Interfaces;

namespace Moasher.Application.Features.Risks.Queries.EditRisk;

public record EditRiskQuery : IRequest<EditRiskDto>
{
    public Guid Id { get; set; }
}

public class EditRiskQueryHandler : IRequestHandler<EditRiskQuery, EditRiskDto>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;

    public EditRiskQueryHandler(IMoasherDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<EditRiskDto> Handle(EditRiskQuery request, CancellationToken cancellationToken)
    {
        var risk = await _context.InitiativeRisks
            .AsNoTracking()
            .Include(r => r.TypeEnum)
            .Include(r => r.PriorityEnum)
            .Include(r => r.ProbabilityEnum)
            .Include(r => r.ImpactEnum)
            .Include(r => r.ScopeEnum)
            .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);

        if (risk is null)
        {
            throw new NotFoundException();
        }
        
        return _mapper.Map<EditRiskDto>(risk);
    }
}