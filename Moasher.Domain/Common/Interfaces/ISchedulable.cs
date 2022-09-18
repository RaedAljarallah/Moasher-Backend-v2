namespace Moasher.Domain.Common.Interfaces;

public interface ISchedulable
{
    public DateTimeOffset PlannedFinish { get; set; }
    public DateTimeOffset? ActualFinish { get; set; }
}