namespace Moasher.Domain.Common.Abstracts;

public abstract class DomainValidator
{
    protected IDictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>();
}