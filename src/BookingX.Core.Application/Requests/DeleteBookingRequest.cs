using System;
using MediatR;

namespace BookingX.Core.Application.Requests
{
    public class DeleteBookingRequest : IRequest<bool>
    {
        public Guid Id { get; private set; }

        public DeleteBookingRequest(Guid id)
        {
            Id = id;
        }
    }
}