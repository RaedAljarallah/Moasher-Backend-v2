namespace Moasher.Application.Common.Behaviours;

// public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest> 
//     where TRequest : notnull
// {
//     private readonly ILogger<LoggingBehaviour<TRequest>> _logger;
//     private readonly ICurrentUserService _currentUser;
//
//     public LoggingBehaviour(ILogger<LoggingBehaviour<TRequest>> logger, ICurrentUserService currentUser)
//     {
//         _logger = logger;
//         _currentUser = currentUser;
//     }
//     
//     public Task Process(TRequest request, CancellationToken cancellationToken)
//     {
//         var requestName = typeof(TRequest).Name;
//         var userId = _currentUser.Id;
//         var email = _currentUser.Email;
//         
//         _logger.LogInformation("Application Request: {Name} {@UserId} {@Email} {@Request}",
//             requestName, userId, email, request);
//         
//         return Task.CompletedTask;
//     }
// }