using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Interfaces;

namespace Moasher.Application.Features.Initiatives.Queries.GetInitiativeDetails;

public record GetInitiativeDetailsQuery : IRequest<InitiativeDetailsDto>
{
    public Guid Id { get; set; }
}

public class GetInitiativeDetailQueryHandler : IRequestHandler<GetInitiativeDetailsQuery, InitiativeDetailsDto>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;

    public GetInitiativeDetailQueryHandler(IMoasherDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<InitiativeDetailsDto> Handle(GetInitiativeDetailsQuery request, CancellationToken cancellationToken)
    {
        var initiative = await _context.Initiatives
            .AsNoTracking()
            .Include(i => i.Teams)
            .ProjectTo<InitiativeDetailsDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(i => i.Id == request.Id, cancellationToken);

        if (initiative is null)
        {
            throw new NotFoundException();
        }

        return initiative;
    }
}