using System.Collections.Generic;
using BookingX.Core.Application.Dtos;
using BookingX.Core.Domain;
using BookingX.Core.Domain.ValueObjects;

namespace BookingX.Core.Application.Interfaces
{
    /// <summary>
    /// IRoomsAvailabilitySolveStrategy. 
    /// </summary>
    public interface IRoomsAvailabilitySolverStrategy
    {
        /// <summary>
        ///  Resolve the rooms availability between an specific date range and based on the already existent bookings.
        /// </summary>
        /// <param name="dateRange">The date range for which availability needs to be resolved.</param>
        /// <param name="rooms">The rooms that need the avaiability to be resolved.</param>
        /// <param name="bookings">The existent bookings.</param>
        /// <returns>A collection of available date ranges per room between the specified date range</returns>
        IEnumerable<RoomAvailabilityDto> Solve(DateRange dateRange, ICollection<Room> rooms, IEnumerable<Booking> bookings);
    }
}