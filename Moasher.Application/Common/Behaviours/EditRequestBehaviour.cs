using MediatR;
using Moasher.Application.Common.Interfaces;

namespace Moasher.Application.Common.Behaviours;

public class EditRequestBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ICurrentUser _currentUser;

    public EditRequestBehaviour(ICurrentUser currentUser)
    {
        _currentUser = currentUser;
    }
    
    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var userName = _currentUser.GetName();
        if (request is not IApprovableCommand)
        {
            return await next();
        }

        var requestType = request.GetType();
        var name = requestType.Name;
        var ggg = requestType.Assembly.FullName;

        return await next();
    }
}