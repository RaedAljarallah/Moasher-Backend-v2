using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Interfaces;

namespace Moasher.Application.Features.Portfolios.Queries.EditPortfolio;

public record EditPortfolioQuery : IRequest<EditPortfolioDto>
{
    public Guid Id { get; set; }
}

public class EditPortfolioQueryHandler : IRequestHandler<EditPortfolioQuery, EditPortfolioDto>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;

    public EditPortfolioQueryHandler(IMoasherDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<EditPortfolioDto> Handle(EditPortfolioQuery request, CancellationToken cancellationToken)
    {
        var portfolio = await _context.Portfolios
            .AsNoTracking()
            .Include(p => p.Initiatives)
            .AsSplitQuery()
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
        
        if (portfolio is null)
        {
            throw new NotFoundException();
        }

        return _mapper.Map<EditPortfolioDto>(portfolio);
    }
}