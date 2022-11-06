using MediatR;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Interfaces;

namespace Moasher.Application.Features.Users.Queries.VerifyActivationToken;

public class VerifyActivationTokenQuery : IRequest<bool>
{
    public string UserId { get; set; }
    public string Token { get; set; } = default!;
}

public class VerifyActivationTokenQueryHandler : IRequestHandler<VerifyActivationTokenQuery, bool>
{
    private readonly IIdentityService _identityService;
    private readonly IUrlCrypter _urlCrypter;

    public VerifyActivationTokenQueryHandler(IIdentityService identityService, IUrlCrypter urlCrypter)
    {
        _identityService = identityService;
        _urlCrypter = urlCrypter;
    }
    
    public async Task<bool> Handle(VerifyActivationTokenQuery request, CancellationToken cancellationToken)
    {
        var id = _urlCrypter.Decode(request.UserId);
        var token = _urlCrypter.Decode(request.Token);
        
        var user = await _identityService.GetUserById(id.ToGuid(), cancellationToken);
        if (user is null)
        {
            return false;
        }

        return await _identityService.VerifyActivationToken(user, token);
    }
}