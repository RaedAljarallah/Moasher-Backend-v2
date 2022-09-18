namespace Moasher.Domain.Common.Interfaces;

public interface IDomainEventHandler
{
    Task ExecuteAsync(params object[] args);
}