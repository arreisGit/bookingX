using System;
using System.Threading.Tasks;

namespace BookingX.Core.Domain.Interfaces
{
    public interface IBookingRepository
    {
        Task<Booking> CreateAsync(Booking booking);
        Task<Booking> GetByIdAsync(Guid id);
        Task<bool> DeleteAsync(Guid id);
    }
}