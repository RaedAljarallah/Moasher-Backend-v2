using MediatR;
using Microsoft.Extensions.Logging;

namespace Moasher.Application.Common.Behaviours;

public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<UnhandledExceptionBehaviour<TRequest, TResponse>> _logger;

    public UnhandledExceptionBehaviour(ILogger<UnhandledExceptionBehaviour<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        try
        {
            return await next();
        }
        catch (TaskCanceledException)
        {
            var requestName = typeof(TRequest).Name;
            _logger.LogError("Task Was Canceled By User for Request {Name}", requestName);
            throw;
        }
        catch (Exception ex)
        {
            var requestName = typeof(TRequest).Name;
            _logger.LogError(ex, "Application Request: Unhandled Exception for Request {Name} {@Request}", requestName,
                request);
            throw;
        }
    }
}