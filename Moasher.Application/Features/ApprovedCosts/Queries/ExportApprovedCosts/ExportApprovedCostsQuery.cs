using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Interfaces;

namespace Moasher.Application.Features.ApprovedCosts.Queries.ExportApprovedCosts;

public record ExportApprovedCostsQuery : IRequest<ExportedApprovedCostsDto>;

public class ExportApprovedCostsQueryHandler : IRequestHandler<ExportApprovedCostsQuery, ExportedApprovedCostsDto>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICsvFileBuilder _csvFileBuilder;

    public ExportApprovedCostsQueryHandler(IMoasherDbContext context, IMapper mapper, ICsvFileBuilder csvFileBuilder)
    {
        _context = context;
        _mapper = mapper;
        _csvFileBuilder = csvFileBuilder;
    }
    
    public async Task<ExportedApprovedCostsDto> Handle(ExportApprovedCostsQuery request, CancellationToken cancellationToken)
    {
        var approvedCosts = await _context.InitiativeApprovedCosts.OrderByDescending(a => a.ApprovalDate)
            .AsNoTracking()
            .ProjectTo<ApprovedCostDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new ExportedApprovedCostsDto("Approved_Costs.csv",
            _csvFileBuilder.BuildApprovedCostsFile(approvedCosts));
    }
}