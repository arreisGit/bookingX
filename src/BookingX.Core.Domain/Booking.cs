using System;
using BookingX.Core.Domain.Exceptions;

namespace BookingX.Core.Domain
{
    public class Booking
    {
        private readonly Guid _id;
        private readonly Guid _customerId;
        private readonly Guid _roomId;

        [JsonProperty("id")]
        public string Id
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

     
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [JsonProperty(_etag)]
        public Guid ETag {get; set;}
    }
}