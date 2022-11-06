using Microsoft.AspNetCore.Identity;

namespace Moasher.Authentication.Core.Identity.Configurations;

public class LocalizedIdentityErrorDescriber : IdentityErrorDescriber
{
    public override IdentityError DuplicateEmail(string email)
    {
        return new IdentityError
        {
            Code = nameof(DuplicateEmail),
            Description = $"البريد الإلكتروني '{email}' مستخدم مسبقاً."
        };
    }

    public override IdentityError DuplicateUserName(string userName)
    {
        return new IdentityError
        {
            Code = nameof(DuplicateUserName),
            Description = $"البريد الإلكتروني '{userName}' مستخدم مسبقاً."
        };
    }

    public override IdentityError PasswordMismatch()
    {
        return new IdentityError
        {
            Code = nameof(PasswordMismatch),
            Description = "كلمة المرور خاطئة."
        };
    }

    public override IdentityError PasswordRequiresDigit()
    {
        return new IdentityError
        {
            Code = nameof(PasswordRequiresDigit),
            Description = "كلمة المرور يجب أن تحتوي على رقم واحد على الأقل ('0' - '9')"
        };
    }

    public override IdentityError PasswordRequiresUpper()
    {
        return new IdentityError
        {
            Code = nameof(PasswordRequiresUpper),
            Description = "كلمة المرور يجب أن تحتوي على حرف كبير واحد على الأقل ('Z' - 'A')"
        };
    }

    public override IdentityError PasswordRequiresLower()
    {
        return new IdentityError
        {
            Code = nameof(PasswordRequiresUpper),
            Description = "كلمة المرور يجب أن تحتوي على حرف صغير واحد على الأقل ('a' - 'z')"
        };
    }

    public override IdentityError PasswordTooShort(int length)
    {
        return new IdentityError
        {
            Code = nameof(PasswordTooShort),
            Description = $"كلمة المرور يجب أن لا تقل عن '{length}' خانات"
        };
    }

    public override IdentityError PasswordRequiresNonAlphanumeric()
    {
        return new IdentityError
        {
            Code = nameof(PasswordRequiresNonAlphanumeric),
            Description = "كلمة المرور يجب أن تحتوي على رمز خاص واحد على الأقل."
        };
    }

    public override IdentityError PasswordRequiresUniqueChars(int uniqueChars)
    {
        return new IdentityError
        {
            Code = nameof(PasswordRequiresUniqueChars),
            Description = $"كلمة المرور يجب أن تحتوي على '{uniqueChars}' عناصر مختلفة."
        };
    }
}