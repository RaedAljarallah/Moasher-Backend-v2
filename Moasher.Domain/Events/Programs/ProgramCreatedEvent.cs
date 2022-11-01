using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Entities;

namespace Moasher.Domain.Events.Programs;

public class ProgramCreatedEvent : DomainEvent
{
    public Program Program { get; }

    public ProgramCreatedEvent(Program program)
    {
        Program = program;
    }
}