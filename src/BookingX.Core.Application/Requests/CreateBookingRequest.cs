using BookingX.Core.Application.Dtos;
using MediatR;

namespace BookingX.Core.Application.Requests
{
    public class CreateBookingRequest : IRequest<BookingDto>
    {
        public BookingDto Booking { get; private set; }

        public CreateBookingRequest(BookingDto booking)
        {
            Booking = booking;
        }
    }
}