using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Entities;
using Moasher.Domain.Validators;

namespace Moasher.Application.Features.Portfolios.Commands.CreatePortfolio;

public record CreatePortfolioCommand : PortfolioCommandBase, IRequest<PortfolioDto>
{
    
}

public class CreatePortfolioCommandHandler : IRequestHandler<CreatePortfolioCommand, PortfolioDto>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;

    public CreatePortfolioCommandHandler(IMoasherDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<PortfolioDto> Handle(CreatePortfolioCommand request, CancellationToken cancellationToken)
    {
        var portfolios = await _context.Portfolios.AsNoTracking().ToListAsync(cancellationToken);
        request.ValidateAndThrow(new PortfolioDomainValidator(portfolios, request.Name, request.Code));
        var portfolio = _mapper.Map<Portfolio>(request);
        
        if (request.InitiativeIds.Any())
        {
            var initiatives = await _context.Initiatives
                .Where(i => request.InitiativeIds.Contains(i.Id))
                .ToListAsync(cancellationToken);
            
            initiatives.ForEach(i => i.Portfolio = portfolio);
            portfolio.Initiatives = initiatives;
        }

        _context.Portfolios.Add(portfolio);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<PortfolioDto>(portfolio);
    }
}