using System;
using BookingX.Core.Domain.Exceptions;

namespace BookingX.Core.Domain
{
    public class Booking
    {
        private readonly Guid _id;
        private readonly Guid _customerId;
        private readonly Guid _roomId;

        public Guid Id
        {
            get => _id;
            init
            {
                _id = value != Guid.Empty ? value : throw new InvalidEntityIdException(nameof(Id));
            }
        }
        public Guid CustomerId
        {
            get => _customerId;
            init
            {
                _customerId = value != Guid.Empty ? value : throw new InvalidEntityIdException(nameof(CustomerId));
            }
        }
        public Guid RoomId
        {
            get => _roomId;
            init
            {
                _roomId = value != Guid.Empty ? value : throw new InvalidEntityIdException(nameof(RoomId));
            }
        }

        public DateTime CreatedUtc { get; } = DateTime.UtcNow;

        // TODO: StartDate cannot be more than 30 days in advance
        // TODO: StartDate must needs to start, at least next day after booking (creation)
        public DateTime StartDate { get; set; }

        // TODO: EndDate cannot be more than 3 days of difference than StartDate
        public DateTime EndDate { get; set; }
    }
}