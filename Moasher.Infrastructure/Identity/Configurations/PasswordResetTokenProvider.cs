using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moasher.Domain.Entities;

namespace Moasher.Infrastructure.Identity.Configurations;

public class PasswordResetTokenProvider : DataProtectorTokenProvider<User>
{
    public PasswordResetTokenProvider(IDataProtectionProvider dataProtectionProvider,
        IOptions<DataProtectionTokenProviderOptions> options, ILogger<PasswordResetTokenProvider> logger) : base(
        dataProtectionProvider, options, logger) { }
}

public class PasswordResetTokenProviderOptions : DataProtectionTokenProviderOptions { }