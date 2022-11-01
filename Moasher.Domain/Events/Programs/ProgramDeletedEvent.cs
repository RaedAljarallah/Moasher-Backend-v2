using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Entities;

namespace Moasher.Domain.Events.Programs;

public class ProgramDeletedEvent : DomainEvent
{
    public Program Program { get; }

    public ProgramDeletedEvent(Program program)
    {
        Program = program;
    }
}