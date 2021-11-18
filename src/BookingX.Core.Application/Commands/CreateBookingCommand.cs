using BookingX.Core.Application.Dtos;
using MediatR;

namespace BookingX.Core.Application.Commands
{
    public class CreateBookingCommand : IRequest
    {
        public BookingDto BookingDto { get; private set; }

        public CreateBookingCommand(BookingDto bookingDto)
        {
            BookingDto = bookingDto;
        }
    }
}