using Microsoft.AspNetCore.Identity;
using Moasher.Domain.Entities;

namespace Moasher.Application.Common.Interfaces;

public interface IIdentityService
{
    public Task<bool> RoleExistsAsync(string roleName, CancellationToken cancellationToken = new());
    public Task<IList<User>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken = new());
    public Task<IList<User>> GetUsersInRolesAsync(IEnumerable<string> roles, CancellationToken cancellationToken = new());
    public Task<bool> UserExistsAsync(string userEmail, CancellationToken cancellationToken = new());
    public Task<User> CreateUserAsync(User user, string password, string role, CancellationToken cancellationToken = new());
    public Task<User?> GetUserById(Guid id, CancellationToken cancellationToken = new());
    public Task<User?> GetUserByEmail(string email, CancellationToken cancellationToken = new());
    public Task<User> UpdateUserAsync(User user, CancellationToken cancellationToken = new());
    public Task DeleteUserAsync(User user, CancellationToken cancellationToken = new());
    public Task<bool> UpdateUserSuspensionStatusAsync(User user, bool status,
        CancellationToken cancellationToken = new());
    public Task<bool> UpdateEmailNotificationStatusAsync(User user, bool status,
        CancellationToken cancellationToken = new());
    public Task<string> UpdateUserRoleAsync(User user, string newRole, CancellationToken cancellationToken = new());
    public Task<string> ResetUserPassword(User user, CancellationToken cancellationToken = new());
    public Task<string> GeneratePassword(CancellationToken cancellationToken = new());
    public Task<string> GenerateActivationToken(User user, CancellationToken cancellationToken = new());
    public Task<bool> VerifyActivationToken(User user, string token);
    public IQueryable<Role> Roles { get; }
    public IQueryable<User> Users { get; }
}