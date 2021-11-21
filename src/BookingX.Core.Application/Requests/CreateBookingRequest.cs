using BookingX.Core.Application.Dtos;
using MediatR;

namespace BookingX.Core.Application.Requests
{
    public class CreateBookingRequest : IRequest<BookingDto>
    {
        public BookingDto BookingDto { get; private set; }

        public CreateBookingRequest(BookingDto bookingDto)
        {
            BookingDto = bookingDto;
        }
    }
}