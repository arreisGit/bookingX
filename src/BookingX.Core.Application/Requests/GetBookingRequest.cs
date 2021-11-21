using System;
using BookingX.Core.Application.Dtos;
using MediatR;

namespace BookingX.Core.Application.Requests
{
    public class GetBookingRequest : IRequest<BookingDto>
    {
        public Guid Id { get; private set; }

        public GetBookingRequest(Guid id)
        {
            Id = id;
        }
    }
}