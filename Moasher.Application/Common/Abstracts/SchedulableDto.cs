namespace Moasher.Application.Common.Abstracts;

public record SchedulableDto
{
    public int All { get; set; }
    public int Planned { get; set; }
    public int Completed { get; set; }
    public int Late { get; set; }
    public int Uncompleted { get; set; }
}