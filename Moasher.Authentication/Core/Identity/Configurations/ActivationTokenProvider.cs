using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Moasher.Authentication.Core.Identity.Entities;

namespace Moasher.Authentication.Core.Identity.Configurations;

public class ActivationTokenProvider : DataProtectorTokenProvider<User>
{
    public ActivationTokenProvider(IDataProtectionProvider dataProtectionProvider,
        IOptions<DataProtectionTokenProviderOptions> options, ILogger<ActivationTokenProvider> logger) : base(
        dataProtectionProvider, options, logger) { }
}

public class ActivationTokenProviderOptions : DataProtectionTokenProviderOptions { }