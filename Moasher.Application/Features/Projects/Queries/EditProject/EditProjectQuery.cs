using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Interfaces;

namespace Moasher.Application.Features.Projects.Queries.EditProject;

public record EditProjectQuery : IRequest<EditProjectDto>
{
    public Guid Id { get; set; }
}

public class EditProjectQueryHandler : IRequestHandler<EditProjectQuery, EditProjectDto>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;

    public EditProjectQueryHandler(IMoasherDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<EditProjectDto> Handle(EditProjectQuery request, CancellationToken cancellationToken)
    {
        var project = await _context.InitiativeProjects
            .AsNoTracking()
            .Include(p => p.PhaseEnum)
            .Include(p => p.Expenditures)
            .Include(p => p.ContractMilestones)
            .ThenInclude(cm => cm.Milestone)
            .AsSplitQuery()
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (project is null)
        {
            throw new NotFoundException();
        }

        return _mapper.Map<EditProjectDto>(project);
    }
}