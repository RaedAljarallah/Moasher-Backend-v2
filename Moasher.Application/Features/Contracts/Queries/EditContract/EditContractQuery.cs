using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Interfaces;

namespace Moasher.Application.Features.Contracts.Queries.EditContract;

public record EditContractQuery : IRequest<EditContractDto>
{
    public Guid Id { get; set; }
}

public class EditContractQueryHandler : IRequestHandler<EditContractQuery, EditContractDto>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;

    public EditContractQueryHandler(IMoasherDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<EditContractDto> Handle(EditContractQuery request, CancellationToken cancellationToken)
    {
        var contract = await _context.InitiativeContracts
            .AsNoTracking()
            .Include(c => c.StatusEnum)
            .Include(c => c.Expenditures)
            .AsSplitQuery()
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
        
        if (contract is null)
        {
            throw new NotFoundException();
        }
        
        return _mapper.Map<EditContractDto>(contract);
    }
}