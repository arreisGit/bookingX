using System;
using BookingX.Core.Domain.Exceptions;
using Newtonsoft.Json;

namespace BookingX.Core.Domain
{
    // TODO: Remove Newtonsoft Attributes.
    // -- POCOS should remain as independent as possible.
    public class Booking
    {
        private readonly Guid _id;
        private readonly Guid _customerId;
        private readonly Guid _roomId;

        [JsonProperty("id")]
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

     
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [JsonProperty("_etag")]
        public string ETag {get; set;}
    }
}