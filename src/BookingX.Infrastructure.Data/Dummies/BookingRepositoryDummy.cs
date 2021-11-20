using System;
using System.Threading.Tasks;
using BookingX.Core.Domain;
using BookingX.Core.Domain.Interfaces;

namespace BookingX.Infrastructure.Data.Dummies
{
    public class BookingRepositoryDummy : IBookingRepository
    {
        public Task<Booking> CreateAsync(Booking booking)
        {
            return Task.FromResult(booking);
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            return Task.FromResult(true);
        }

        public Task<bool> UpdateAsync(Booking booking)
        {
            return Task.FromResult(true);
        }

        public Task<Booking> GetByIdAsync(Guid id)
        {
            var fakeBooking = new Booking
            {
                Id = id,
                RoomId = Guid.NewGuid(),
                CustomerId = Guid.NewGuid(),
                StartDate = DateTime.UtcNow.Date,
                EndDate = DateTime.UtcNow.Date.AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(999)

            };

            return Task.FromResult(fakeBooking);
        }
    }
}