using Moasher.Application.Common.Abstracts;

namespace Moasher.Application.Features.Users.Commands.ResetUserPassword;

public record ResetUserPasswordEmailModel(string FullName, string TempPassword) : EmailModelBase;