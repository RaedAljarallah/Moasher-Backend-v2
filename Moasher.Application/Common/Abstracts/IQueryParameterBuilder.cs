using Moasher.Domain.Common.Interfaces;

namespace Moasher.Application.Common.Abstracts;

public interface IQueryParameterBuilder<TEntity> where TEntity : IDbEntity
{
    public IQueryable<TEntity> Build(IQueryable<TEntity> query);
}