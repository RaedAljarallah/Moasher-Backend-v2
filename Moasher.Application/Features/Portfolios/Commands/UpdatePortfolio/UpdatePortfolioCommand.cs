using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Events.Portfolios;
using Moasher.Domain.Validators;

namespace Moasher.Application.Features.Portfolios.Commands.UpdatePortfolio;

public record UpdatePortfolioCommand : PortfolioCommandBase, IRequest<PortfolioDto>
{
    public Guid Id { get; set; }
}

public class UpdatePortfolioCommandHandler : IRequestHandler<UpdatePortfolioCommand, PortfolioDto>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;

    public UpdatePortfolioCommandHandler(IMoasherDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<PortfolioDto> Handle(UpdatePortfolioCommand request, CancellationToken cancellationToken)
    {
        var portfolios = await _context.Portfolios
            .AsNoTracking()
            .Include(i => i.Initiatives.Where(initiative => initiative.PortfolioId == request.Id))
            .ToListAsync(cancellationToken);
        var portfolio = portfolios.FirstOrDefault(p => p.Id == request.Id);
        if (portfolio is null)
        {
            throw new NotFoundException();
        }
        
        request.ValidateAndThrow(new PortfolioDomainValidator(portfolios.Where(e => e.Id != request.Id).ToList(), request.Name, request.Code));

        if (!request.InitiativeIds.SequenceEqual(portfolio.Initiatives.Select(i => i.Id).ToList()))
        {
            // To Remove PortfolioName from all portfolios initiatives
            portfolio.Initiatives.ToList().ForEach(i => i.Portfolio = null);
            portfolio.Initiatives.Clear();
            var initiatives = await _context.Initiatives
                .Where(i => request.InitiativeIds.Contains(i.Id))
                .ToListAsync(cancellationToken);
            initiatives.ForEach(i => i.Portfolio = portfolio);
            portfolio.Initiatives = initiatives;
        }
        
        var hasEvent = request.Name != portfolio.Name;

        _mapper.Map(request, portfolio);
        if (hasEvent)
        {
            portfolio.AddDomainEvent(new PortfolioUpdatedEvent(portfolio));
        }

        _context.Portfolios.Update(portfolio);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<PortfolioDto>(portfolio);
    }
}