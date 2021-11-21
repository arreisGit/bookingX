using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookingX.Core.Domain.ValueObjects;

namespace BookingX.Core.Domain.Interfaces
{
    /// <summary>
    /// IBookingRepository
    /// </summary>
    public interface IBookingRepository
    {
        /// <summary>
        /// Creates a Booking
        /// </summary>
        /// <param name="booking">The Booking to be created</param>
        /// <returns>The resulting Booking</returns>
        Task<Booking> CreateAsync(Booking booking);

        /// <summary>
        /// Gets the Booking by Id
        /// </summary>
        /// <param name="id">The Booking Id</param>
        /// <returns>Booking</returns>
        Task<Booking> GetByIdAsync(Guid id);

        /// <summary>
        /// Update a booking
        /// </summary>
        /// <param name="booking">The Booking to be updated</param>
        /// <returns>True if the update process succeeds, false otherwise.</returns>
        Task<bool> UpdateAsync(Booking booking);
        
        /// <summary>
        /// Delete a booking
        /// </summary>
        /// <param name="booking">The Booking to be deleted</param>
        /// <returns>True if the deletion succeeds, false otherwhise.</returns>
        Task<bool> DeleteAsync(Guid id);

        /// <summary>
        /// Searches for all existent bookings between a date range and returns them
        /// </summary>
        /// <param name="dateRange">The date range that will be used to look for bookings</param>
        /// <param name="roomId"> (Optional) The room id to restrict the search to</param>
        Task<IEnumerable<Booking>> GetAllBookingsInDateRange(DateRange dateRange);

        /// <summary>
        /// Searches for all existent bookings made for a specific room,  between a date range and returns them
        /// </summary>
        /// <param name="roomId">The room id to restrict the search to</param>
        /// <param name="dateRange">The date range that will be used to look for bookings</param>
        Task<IEnumerable<Booking>> GetRoomBookingsInDateRange(Guid roomId, DateRange dateRange);
    }
}