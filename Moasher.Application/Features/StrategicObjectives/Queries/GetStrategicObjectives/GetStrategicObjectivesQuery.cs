using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Abstracts;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Interfaces;
using Moasher.Application.Common.Types;
using Moasher.Domain.Entities.StrategicObjectiveEntities;

namespace Moasher.Application.Features.StrategicObjectives.Queries.GetStrategicObjectives;

public record GetStrategicObjectivesQuery : QueryParameterBase, IRequest<object>
{
    public string? Name { get; set; }
    public string? Code { get; set; }
    public short Level { get; set; } = 1;
    public string? DescendantOf { get; set; }
    public Guid? ParentId { get; set; }
    public Guid? EntityId { get; set; }
    public Guid? ProgramId { get; set; }
}

public class GetStrategicObjectivesQueryHandler : IRequestHandler<GetStrategicObjectivesQuery, object>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;

    public GetStrategicObjectivesQueryHandler(IMoasherDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<object> Handle(GetStrategicObjectivesQuery request, CancellationToken cancellationToken)
    {
        object strategicObjectives = new PaginatedList<object>(new List<object>(), 0, request.PageNumber, request.PageSize);

        if (request.ParentId.HasValue)
        {
            var parentObjective = await _context.StrategicObjectives
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.Id == request.ParentId, cancellationToken);

            if (parentObjective is null)
            {
                throw new NotFoundException();
            }

            request.DescendantOf = parentObjective.HierarchyId.ToString();
        }

        var strategicObjectivesQuery = _context.StrategicObjectives
            .AsNoTracking()
            .WithinParameters(new GetStrategicObjectivesQueryParameter(request));

        strategicObjectives = request.Level switch
        {
            1 => await GenerateLevelOneQuery(strategicObjectivesQuery)
                .ProjectTo<StrategicObjectiveLevelOneDto>(_mapper.ConfigurationProvider)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken),
            2 => await GenerateLevelTwoQuery(strategicObjectivesQuery)
                .ProjectTo<StrategicObjectiveLevelTwoDto>(_mapper.ConfigurationProvider)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken),
            3 => await GenerateLevelThreeQuery(strategicObjectivesQuery)
                .ProjectTo<StrategicObjectiveLevelThreeDto>(_mapper.ConfigurationProvider)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken),
            4 => await GenerateLevelFourQuery(strategicObjectivesQuery)
                .ProjectTo<StrategicObjectiveLevelFourDto>(_mapper.ConfigurationProvider)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken),
            _ => strategicObjectives
        };

        return strategicObjectives;
    }

    private IQueryable<StrategicObjectiveLevelFour> GenerateLevelFourQuery(
        IQueryable<StrategicObjective> strategicObjectivesQuery)
    {
        return strategicObjectivesQuery.Select(o => new StrategicObjectiveLevelFour
        {
            Id = o.Id,
            Name = o.Name,
            Code = o.Code,
            HierarchyId = o.HierarchyId,
            CreatedBy = o.CreatedBy,
            CreatedAt = o.CreatedAt,
            LastModifiedBy = o.LastModifiedBy,
            LastModified = o.LastModified,
            LevelOne =
                _context.StrategicObjectives.FirstOrDefault(l1 => l1.HierarchyId == o.HierarchyId.GetAncestor(3))!,
            LevelTwo =
                _context.StrategicObjectives.FirstOrDefault(l2 => l2.HierarchyId == o.HierarchyId.GetAncestor(2))!,
            LevelThree =
                _context.StrategicObjectives.FirstOrDefault(l3 => l3.HierarchyId == o.HierarchyId.GetAncestor(1))!,
            InitiativesCount = _context.Initiatives.Count(i => i.LevelFourStrategicObjectiveId == o.Id),
            KPIsCount = _context.KPIs.Count(k => k.LevelFourStrategicObjectiveId == o.Id)
        });
    }

    private IQueryable<StrategicObjectiveLevelThree> GenerateLevelThreeQuery(
        IQueryable<StrategicObjective> strategicObjectivesQuery)
    {
        return strategicObjectivesQuery.Select(o => new StrategicObjectiveLevelThree
        {
            Id = o.Id,
            Name = o.Name,
            Code = o.Code,
            HierarchyId = o.HierarchyId,
            CreatedBy = o.CreatedBy,
            CreatedAt = o.CreatedAt,
            LastModifiedBy = o.LastModifiedBy,
            LastModified = o.LastModified,
            LevelOne =
                _context.StrategicObjectives.FirstOrDefault(l1 => l1.HierarchyId == o.HierarchyId.GetAncestor(2))!,
            LevelTwo =
                _context.StrategicObjectives.FirstOrDefault(l2 => l2.HierarchyId == o.HierarchyId.GetAncestor(1))!,
            LevelFourCount = _context.StrategicObjectives.Count(l4 => l4.HierarchyId.GetAncestor(1) == o.HierarchyId),
            InitiativesCount = o.Initiatives.Count,
            KPIsCount = o.KPIs.Count
        });
    }

    private IQueryable<StrategicObjectiveLevelTwo> GenerateLevelTwoQuery(
        IQueryable<StrategicObjective> strategicObjectivesQuery)
    {
        return strategicObjectivesQuery.Select(o => new StrategicObjectiveLevelTwo
        {
            Id = o.Id,
            Name = o.Name,
            Code = o.Code,
            HierarchyId = o.HierarchyId,
            CreatedBy = o.CreatedBy,
            CreatedAt = o.CreatedAt,
            LastModifiedBy = o.LastModifiedBy,
            LastModified = o.LastModified,
            LevelOne =
                _context.StrategicObjectives.FirstOrDefault(l1 => l1.HierarchyId == o.HierarchyId.GetAncestor(1))!,
            LevelThreeCount = _context.StrategicObjectives.Count(l3 => l3.HierarchyId.GetAncestor(1) == o.HierarchyId),
            LevelFourCount = _context.StrategicObjectives.Count(l4 => l4.HierarchyId.GetAncestor(2) == o.HierarchyId),
            InitiativesCount = _context.StrategicObjectives.Where(l3 => l3.HierarchyId.GetAncestor(1) == o.HierarchyId)
                .SelectMany(ob => ob.Initiatives).Count(),
            KPIsCount = _context.StrategicObjectives.Where(l3 => l3.HierarchyId.GetAncestor(1) == o.HierarchyId)
                .SelectMany(ob => ob.KPIs).Count()
        });
    }

    private IQueryable<StrategicObjectiveLevelOne> GenerateLevelOneQuery(
        IQueryable<StrategicObjective> strategicObjectivesQuery)
    {
        return strategicObjectivesQuery.Select(o => new StrategicObjectiveLevelOne
        {
            Id = o.Id,
            Name = o.Name,
            Code = o.Code,
            HierarchyId = o.HierarchyId,
            CreatedBy = o.CreatedBy,
            CreatedAt = o.CreatedAt,
            LastModifiedBy = o.LastModifiedBy,
            LastModified = o.LastModified,
            LevelTwoCount = _context.StrategicObjectives.Count(l2 => l2.HierarchyId.GetAncestor(1) == o.HierarchyId),
            LevelThreeCount = _context.StrategicObjectives.Count(l3 => l3.HierarchyId.GetAncestor(2) == o.HierarchyId),
            LevelFourCount = _context.StrategicObjectives.Count(l4 => l4.HierarchyId.GetAncestor(3) == o.HierarchyId),
            InitiativesCount = _context.StrategicObjectives.Where(l3 => l3.HierarchyId.GetAncestor(2) == o.HierarchyId)
                .SelectMany(ob => ob.Initiatives).Count(),
            KPIsCount = _context.StrategicObjectives.Where(l3 => l3.HierarchyId.GetAncestor(2) == o.HierarchyId)
                .SelectMany(ob => ob.KPIs).Count()
        });
    }
}