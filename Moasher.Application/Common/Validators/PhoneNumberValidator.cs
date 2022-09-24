using FluentValidation;

namespace Moasher.Application.Common.Validators;

public static class PhoneNumberValidator
{
    public static IRuleBuilderOptions<T, string> PhoneNumber<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.Matches(@"^0[0-9]{9}$").WithMessage("رقم الهاتف يجب أن يكون من 10 خانات ويبدأ بـ 0");
    }
}