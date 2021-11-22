using BookingX.Core.Application.FluentValidation;
using BookingX.Core.Application.Requests;
using FluentValidation;

namespace BookingX.Core.Application.RequestValidators
{
    public class UpdateBookingRequestValidator : AbstractValidator<UpdateBookingRequest>
    {
        public UpdateBookingRequestValidator()
        {
            RuleFor(r => r.Booking).SetValidator(new BookingDtoValidator());
        }
    }
}