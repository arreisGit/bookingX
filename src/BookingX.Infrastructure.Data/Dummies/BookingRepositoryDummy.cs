using System;
using System.Threading.Tasks;
using BookingX.Core.Domain;
using BookingX.Core.Domain.Interfaces;

namespace BookingX.Infrastructure.Data.Dummies
{
    public class BookingRepositoryDummy : IBookingRepository
    {
        public Task<Booking> CreateBooking(Booking booking)
        {
            return Task.FromResult(booking);
        }

        public Task<Booking> GetById(Guid id)
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