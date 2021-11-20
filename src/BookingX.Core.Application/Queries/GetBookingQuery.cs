using System;
using BookingX.Core.Application.Dtos;
using MediatR;

namespace BookingX.Core.Application.Queries
{
    public class GetBookingQuery : IRequest<BookingDto>
    {
        
        public Guid Id {get; private set;}

        public GetBookingQuery(Guid id)
        {
            Id = id;
        }
    }
}