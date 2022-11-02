using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Interfaces;

namespace Moasher.Application.Features.Initiatives.Queries.ExportInitiatives;

public record ExportInitiativesQuery : IRequest<ExportedInitiativesDto>;

public class ExportInitiativesQueryHandler : IRequestHandler<ExportInitiativesQuery, ExportedInitiativesDto>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICsvFileBuilder _csvFileBuilder;

    public ExportInitiativesQueryHandler(IMoasherDbContext context, IMapper mapper, ICsvFileBuilder csvFileBuilder)
    {
        _context = context;
        _mapper = mapper;
        _csvFileBuilder = csvFileBuilder;
    }
    
    public async Task<ExportedInitiativesDto> Handle(ExportInitiativesQuery request, CancellationToken cancellationToken)
    {
        var initiatives = await _context.Initiatives
            .AsNoTracking()
            .ProjectTo<InitiativeDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new ExportedInitiativesDto("Initiatives.csv", _csvFileBuilder.BuildInitiativesFile(initiatives));
    }
}