using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Interfaces;

namespace Moasher.Application.Features.Contracts.Queries.ExportContracts;

public record ExportContractsQuery : IRequest<ExportedContractsDto>;

public class ExportContractsQueryHandler : IRequestHandler<ExportContractsQuery, ExportedContractsDto>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICsvFileBuilder _csvFileBuilder;

    public ExportContractsQueryHandler(IMoasherDbContext context, IMapper mapper, ICsvFileBuilder csvFileBuilder)
    {
        _context = context;
        _mapper = mapper;
        _csvFileBuilder = csvFileBuilder;
    }
    
    public async Task<ExportedContractsDto> Handle(ExportContractsQuery request, CancellationToken cancellationToken)
    {
        var contracts = await _context.InitiativeContracts.OrderBy(c => c.StartDate).ThenBy(c => c.Name)
            .AsNoTracking()
            .Include(c => c.Expenditures)
            .ProjectTo<ContractDto>(_mapper.ConfigurationProvider)
            .AsSplitQuery()
            .ToListAsync(cancellationToken);

        return new ExportedContractsDto("Contracts.csv", _csvFileBuilder.BuildContractsFile(contracts));
    }
}