namespace Moasher.Application.Common.Interfaces;

public interface IBackgroundJob
{
    Task ExecuteAsync(params object[] args);
}