using BookingX.Core.Application.FluentValidation;
using BookingX.Core.Application.Requests;
using FluentValidation;

namespace BookingX.Core.Application.RequestValidators
{
    public class GetRoomsAvailabilityRequestValidator : AbstractValidator<CreateBookingRequest>
    {
        public GetRoomsAvailabilityRequestValidator()
        {
            RuleFor(r => r.Booking).SetValidator(new BookingDtoValidator());
        }
    }
}