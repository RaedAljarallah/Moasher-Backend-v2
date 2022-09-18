using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Entities;

namespace Moasher.Domain.Events.Programs;

public class ProgramUpdatedEvent : DomainEvent
{
    public Program Program { get; }

    public ProgramUpdatedEvent(Program program)
    {
        Program = program;
    }
}