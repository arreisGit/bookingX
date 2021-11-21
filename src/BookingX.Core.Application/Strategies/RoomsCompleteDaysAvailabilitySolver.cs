using System;
using System.Collections.Generic;
using System.Linq;
using BookingX.Core.Application.Dtos;
using BookingX.Core.Application.Interfaces;
using BookingX.Core.Domain;
using BookingX.Core.Domain.ValueObjects;

namespace BookingX.Core.Application.Strategies
{
    /// <summary>
    /// Rooms availability solver class.
    /// </summary>
    public class RoomsCompleteDaysAvailabilitySolver : IRoomsAvailabilitySolverStrategy

    {
        /// <summary>
        /// Initializes a new instance of <see cref="RoomsWholeDaysAvailabilitySolver"/> class.
        /// </summary>
        public RoomsCompleteDaysAvailabilitySolver() { }

        /// <inheritdoc />
        public IEnumerable<RoomAvailabilityDto> Solve(
            DateRange dateRange,
            ICollection<Room> rooms,
            IEnumerable<Booking> bookings)
        {
            if (rooms == null || !rooms.Any())
                throw new ArgumentException($"{nameof(rooms)} cannot be null or empty");

            List<RoomAvailabilityDto> roomsAvailability = new List<RoomAvailabilityDto>();

            foreach (var room in rooms)
            {
                IOrderedEnumerable<Booking> roomBookings = GetRoomBookings(dateRange, bookings, room);

                IEnumerable<DateRange> availableRoomDateRanges = roomBookings.Any() ?
                                            AvailableDateRangesInScope(dateRange, roomBookings)
                                        : new DateRange[] { new DateRange (dateRange.From, dateRange.To.AddDays(1).AddMilliseconds(-1))};

                roomsAvailability.Add(new RoomAvailabilityDto(room.Id, availableRoomDateRanges));

            }

            return roomsAvailability;
        }

        private static IOrderedEnumerable<Booking> GetRoomBookings(
            DateRange dateRange,
            IEnumerable<Booking> bookings,
            Room room)
        {
            return bookings
                    .Where(b =>
                    {
                        return b.RoomId.Equals(room.Id)
                            && (
                                (b.StartDate >= dateRange.From && b.StartDate <= dateRange.To)
                             || (b.EndDate >= dateRange.From && b.EndDate <= dateRange.To)
                             || (b.StartDate < dateRange.From && b.EndDate > dateRange.To)
                            );
                    })
                    .OrderBy(b => b.StartDate);
        }

        private IEnumerable<DateRange> AvailableDateRangesInScope(
            DateRange dateRange,
            IOrderedEnumerable<Booking> roomBookings)
        {
            List<DateRange> availableDateRanges = new List<DateRange>();

            DateTime currentDate = dateRange.From.Date;
            
            foreach(var booking in roomBookings)
            {
                if(booking.StartDate.Date > currentDate.Date)
                {
                    availableDateRanges.Add(
                        new DateRange(
                            currentDate,
                            booking.StartDate.Date.AddMilliseconds(-1)
                        )
                    );

                    currentDate = booking.EndDate.Date.AddDays(1);
                }
                else{
                    currentDate = booking.EndDate.Date.AddDays(1);
                }
                 
            }

            if(currentDate < dateRange.To)
            {
                availableDateRanges.Add(
                    new DateRange(
                        currentDate,
                        dateRange.To.Date.AddDays(1).AddMilliseconds(-1) 
                    )
                );

            }

            return availableDateRanges;
        }
    }
}