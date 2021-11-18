using System.Threading.Tasks;
using BookingX.Core.Domain;
using BookingX.Core.Domain.Interfaces;

namespace BookingX.Infrastructure.Data.Dummies
{
    public class BookingRepositoryDummy : IBookingRepository
    {
        public Task CreateBooking(Booking booking)
        {
            return Task.CompletedTask;
        }
    }
}