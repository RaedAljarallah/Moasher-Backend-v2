using Moasher.Domain.Common.Abstracts;

namespace Moasher.Application.Common.Abstracts;

public interface IQueryParameterBuilder<TEntity> where TEntity : DbEntity
{
    public IQueryable<TEntity> Build(IQueryable<TEntity> query);
}