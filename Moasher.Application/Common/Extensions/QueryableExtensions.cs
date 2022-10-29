using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Abstracts;
using Moasher.Application.Common.Types;
using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Common.Interfaces;

namespace Moasher.Application.Common.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<TEntity> WithinParameters<TEntity>(this IQueryable<TEntity> query,
        IQueryParameterBuilder<TEntity> parameters)
        where TEntity : IDbEntity
    {
        return parameters.Build(query);
    }

    public static IQueryable<TEntity> Like<TEntity>(this IQueryable<TEntity> query, string searchQuery,
        params string[] fields) where TEntity : IDbEntity
    {
        var predicate = fields.Aggregate(string.Empty, (current, field) => !string.IsNullOrEmpty(current)
            ? $"{current} || {field}.Contains(@0)"
            : $"{field}.Contains(@0)");

        return query.Where(ParsingConfig.DefaultEFCore21, predicate, searchQuery);
    }

    public static IQueryable<TEntity> IncludeWhen<TEntity, TProperty>(this IQueryable<TEntity> query,
        Expression<Func<TEntity, TProperty>> navigationPropertyPath, bool shouldInclude)
        where TEntity : DbEntity
    {
        return shouldInclude
            ? query.Include(navigationPropertyPath)
            : query;
    }

    public static async Task<PaginatedList<TResult>> ToPaginatedListAsync<TResult>(this IQueryable<TResult> query,
        int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        var count = await query.CountAsync(cancellationToken);
        if (pageSize > 0)
        {
            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }

        var items = await query.ToListAsync(cancellationToken);

        return new PaginatedList<TResult>(items, count, pageNumber, pageSize);
    }

    public static async Task<PaginatedList<TResult>> ToPaginatedListAsync<TSource, TResult>(
        this IQueryable<TSource> query, Expression<Func<TSource, TResult>> selector, int pageNumber, int pageSize,
        CancellationToken cancellationToken = default)
    {
        var count = await query.CountAsync(cancellationToken);

        if (pageSize > 0)
        {
            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }

        var items = await query.Select(selector).ToListAsync(cancellationToken);

        return new PaginatedList<TResult>(items, count, pageNumber, pageSize);
    }

    public static Task<List<TResult>> ProjectToListAsync<TSource, TResult>(this IQueryable<TSource> query,
        Expression<Func<TSource, TResult>> selector, CancellationToken cancellationToken = default)
    {
        return query.Select(selector).ToListAsync(cancellationToken);
    }
}