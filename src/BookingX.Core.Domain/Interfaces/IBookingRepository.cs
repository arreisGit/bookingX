using System.Threading.Tasks;

namespace BookingX.Core.Domain.Interfaces
{
    public interface IBookingRepository
    {
        Task CreateBooking(Booking booking);
    }
}