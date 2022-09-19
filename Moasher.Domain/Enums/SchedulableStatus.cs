namespace Moasher.Domain.Enums;

public enum SchedulableStatus : byte
{
    Planned = 1,
    Completed,
    Late,
    Uncompleted,
    Due
}