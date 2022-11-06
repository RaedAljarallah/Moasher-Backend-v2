using Microsoft.AspNetCore.Identity;
using Moasher.Domain.Entities;

namespace Moasher.Application.Common.Interfaces;

public interface IIdentityService
{
    public Task<bool> RoleExistsAsync(string roleName, CancellationToken cancellationToken = default);
    public Task<IList<User>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken = default);
    
    public Task<bool> UserExistsAsync(string userEmail, CancellationToken cancellationToken = default);
    public Task<User> CreateUserAsync(User user, string password, string role, CancellationToken cancellationToken = default);
    public Task<User?> GetUserById(Guid id, CancellationToken cancellationToken = default);
    public Task<User> UpdateUserAsync(User user, CancellationToken cancellationToken = default);
    public Task DeleteUserAsync(User user, CancellationToken cancellationToken = default);
    public Task<bool> UpdateUserSuspensionStatusAsync(User user, bool status,
        CancellationToken cancellationToken = default);
    public Task<string> UpdateUserRoleAsync(User user, string newRole, CancellationToken cancellationToken = default);
    public Task<string> ResetUserPassword(User user, CancellationToken cancellationToken = default);
    public Task<string> GeneratePassword(CancellationToken cancellationToken = default);
    public Task<string> GenerateActivationToken(User user, CancellationToken cancellationToken = default);
    public Task<bool> VerifyActivationToken(User user, string token);
    public IQueryable<Role> Roles { get; }
    public IQueryable<User> Users { get; }
}