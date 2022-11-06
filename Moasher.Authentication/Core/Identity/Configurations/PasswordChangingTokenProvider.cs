using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Moasher.Authentication.Core.Identity.Entities;

namespace Moasher.Authentication.Core.Identity.Configurations;

public class PasswordChangingTokenProvider : DataProtectorTokenProvider<User>
{
    public PasswordChangingTokenProvider(IDataProtectionProvider dataProtectionProvider,
        IOptions<DataProtectionTokenProviderOptions> options, ILogger<PasswordChangingTokenProvider> logger) : base(
        dataProtectionProvider, options, logger) { }
}

public class PasswordChangingTokenProviderOptions : DataProtectionTokenProviderOptions { }