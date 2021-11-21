using System;
using BookingX.Core.Application.Dtos;
using MediatR;

namespace BookingX.Core.Application.Requests
{
    public class UpdateBookingRequest : IRequest<bool>
    {
        public BookingDto Booking { get; }
        
        public UpdateBookingRequest(BookingDto booking)
        {
            Booking = booking;
        }
    }
}