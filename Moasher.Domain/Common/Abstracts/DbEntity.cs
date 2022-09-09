namespace Moasher.Domain.Common.Abstracts;

public abstract class DbEntity<TKey> where TKey : IEquatable<TKey>
{
    public TKey Id { get; set; } = default!;
}