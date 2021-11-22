using BookingX.Core.Application.FluentValidation;
using BookingX.Core.Application.Requests;
using FluentValidation;

namespace BookingX.Core.Application.RequestValidators
{
    public class CreateBookingRequestValidator : AbstractValidator<CreateBookingRequest>
    {
        public CreateBookingRequestValidator()
        {
            RuleFor(r => r.Booking).SetValidator(new BookingDtoValidator());
        }
    }
}