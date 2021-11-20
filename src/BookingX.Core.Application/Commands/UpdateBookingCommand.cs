using System;
using BookingX.Core.Application.Dtos;
using MediatR;

namespace BookingX.Core.Application.Commands
{
    public class UpdateBookingCommand : IRequest<bool>
    {
        public BookingDto Booking { get; }
        
        public UpdateBookingCommand(BookingDto booking)
        {
            Booking = booking;
        }
    }
}