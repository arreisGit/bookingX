using BookingX.Core.Application.FluentValidation;
using BookingX.Core.Application.Requests;
using FluentValidation;

namespace BookingX.Core.Application.RequestValidators
{
    public class DeleteBookingRequestValidator : AbstractValidator<DeleteBookingRequest>
    {
        public DeleteBookingRequestValidator()
        {
            RuleFor(r => r.Id).NotEmpty();
        }
    }
}