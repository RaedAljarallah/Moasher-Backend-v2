using Moasher.Application.Common.Extensions;

namespace Moasher.Application.Features.Users.Commands;

public abstract record UserCommandBase
{
    private string _firstName = default!;
    private string _lastName = default!;
    private string _email = default!;
    private string _phoneNumber = default!;
    private string _role = default!;

    public string FirstName { get => _firstName; set => _firstName = value.Trim(); }
    public string LastName { get => _lastName; set => _lastName = value.Trim(); }
    public string Email { get => _email; set => _email = value.Trim(); }
    public string PhoneNumber { get => _phoneNumber; set => _phoneNumber = value.Trim(); }
    public string Role { get => _role; set => _role = value.Trim().FirstCharToUpper(); }
    public Guid EntityId { get; set; }
}