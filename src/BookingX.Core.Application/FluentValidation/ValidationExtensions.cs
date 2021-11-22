using FluentValidation;

namespace BookingX.Core.Application.FluentValidation
{
    public static class ValidationExtensions
    {
        public static IRuleBuilderOptions<T, string> IsValidDateTime<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new StringDateTimeValidator<T>());
        }
    }
}