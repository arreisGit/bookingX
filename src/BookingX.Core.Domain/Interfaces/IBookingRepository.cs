using System;
using System.Threading.Tasks;

namespace BookingX.Core.Domain.Interfaces
{
    public interface IBookingRepository
    {
        Task<Booking> CreateBooking(Booking booking);
        Task<Booking> GetById(Guid id);
    }
}