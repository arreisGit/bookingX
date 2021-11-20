using System;
using BookingX.Core.Application.Strategies;
using BookingX.Core.Domain;
using BookingX.Core.Domain.ValueObjects;
using Xunit;

namespace BookingX.Core.Application.Tests.Strategies
{
    public class RoomAvailabilitySolverUnitTests
    {
        [Fact]
        public void Solve_Given_No_Provided_Rooms_Throw_ArgumentException()
        {
            // Arrange 
            var dateRange = new DateRange(DateTime.Today, DateTime.Today.AddDays(1));
            var emptyBookings = Array.Empty<Booking>();

            var availabilitySolver = new RoomsAvailabilitySolver();

            // Act && Assert
            Assert.Throws<ArgumentException>(() =>
            {
                return availabilitySolver.Solve(dateRange, null, emptyBookings);
            });
        }
    }
}