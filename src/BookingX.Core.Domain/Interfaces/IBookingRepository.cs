using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookingX.Core.Domain.ValueObjects;

namespace BookingX.Core.Domain.Interfaces
{
    public interface IBookingRepository
    {
        Task<Booking> CreateAsync(Booking booking);
        Task<Booking> GetByIdAsync(Guid id);
        Task<bool> UpdateAsync(Booking booking);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<Booking>> GetAllBookingsInDateRange(DateRange dateRange);
    }
}