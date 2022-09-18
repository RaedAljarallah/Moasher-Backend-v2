using Moasher.Domain.Common.Abstracts;

namespace Moasher.Domain.ValueObjects;

public class Progress : ValueObject
{
    public float Planned { get; }
    public float Actual { get; }
    public Progress() { }
    public Progress(float planned, float actual)
    {
        Planned = planned;
        Actual = actual;
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Planned;
        yield return Actual;
    }
}