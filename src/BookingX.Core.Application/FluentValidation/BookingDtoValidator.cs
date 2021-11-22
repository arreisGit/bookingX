using System;
using BookingX.Core.Application.Dtos;
using FluentValidation;

namespace BookingX.Core.Application.FluentValidation
{
    public class BookingDtoValidator : AbstractValidator<BookingDto>
    {
        public BookingDtoValidator()
        {
            RuleFor(b => b.Id).NotEmpty();
            RuleFor(b => b.CustomerId).NotEmpty();
            RuleFor(b => b.RoomId).NotEmpty();
            RuleFor(b => b.StartDate).NotEmpty().IsValidDateTime();
            RuleFor(b => b.EndDate).NotEmpty().IsValidDateTime();
        }
        private bool BeAValidDate(string value)
        {
            return DateTime.TryParse(value, out DateTime _);
        }
    }
}