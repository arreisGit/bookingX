using System;
using FluentValidation;
using FluentValidation.Validators;

namespace BookingX.Core.Application.FluentValidation
{
    public class StringDateTimeValidator<T> : PropertyValidator<T, string>
    {
        public override string Name => "StringDateTimeValidator";
        protected override string GetDefaultMessageTemplate(string errorCode)
            => "'{PropertyValue}' is not a valid date";
        public override bool IsValid(ValidationContext<T> context, string value)
        {
            if (value == null) return true;

            if (value as string == null) return false;

            return DateTime.TryParse(value as string, out DateTime _);
        }

    }
}