using System;
using MediatR;

namespace BookingX.Core.Application.Commands
{
    public class DeleteBookingCommand : IRequest<bool>
    {
        public Guid Id { get; private set; }

        public DeleteBookingCommand(Guid id)
        {
            Id = id;
        }
    }
}