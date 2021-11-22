using BookingX.Core.Application.FluentValidation;
using BookingX.Core.Application.Requests;
using FluentValidation;

namespace BookingX.Core.Application.RequestValidators
{
    public class GetBookingRequestValidator : AbstractValidator<GetBookingRequest>
    {
        public GetBookingRequestValidator()
        {
            RuleFor(r => r.Id).NotEmpty();
        }
    }
}