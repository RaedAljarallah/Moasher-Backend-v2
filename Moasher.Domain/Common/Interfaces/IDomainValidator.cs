namespace Moasher.Domain.Common.Interfaces;

public interface IDomainValidator
{
    public IDictionary<string, string[]> Validate();
}