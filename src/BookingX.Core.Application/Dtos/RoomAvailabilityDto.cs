using System;
using System.Collections.Generic;
using BookingX.Core.Domain.ValueObjects;

namespace BookingX.Core.Application.Dtos
{
    public class RoomAvailabilityDto
    {
        public Guid RoomId { get; private set; }

        public IEnumerable<DateRange> AvailableDateRanges { get; private set; }

        public RoomAvailabilityDto(Guid roomId, IEnumerable<DateRange> availableDateRanges )
        {
            RoomId = roomId;
            AvailableDateRanges = availableDateRanges ?? throw new ArgumentNullException(nameof(availableDateRanges));
        }
    }
}