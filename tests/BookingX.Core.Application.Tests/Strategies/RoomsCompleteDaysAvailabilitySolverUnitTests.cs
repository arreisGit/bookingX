using System;
using System.Linq;
using BookingX.Core.Application.Strategies;
using BookingX.Core.Domain;
using BookingX.Core.Domain.ValueObjects;
using Xunit;

namespace BookingX.Core.Application.Tests.Strategies
{
    public class RoomsCompleteDaysAvailabilitySolverUnitTests
    {
        [Fact]
        public void Solve_Given_No_Provided_Rooms_Throw_ArgumentException()
        {
            // Arrange 
            var dateRange = new DateRange(DateTime.Today, DateTime.Today.AddDays(1));
            var emptyBookings = Array.Empty<Booking>();

            var availabilitySolver = new RoomsCompleteDaysAvailabilitySolver();

            // Act && Assert
            Assert.Throws<ArgumentException>(() =>
            {
                return availabilitySolver.Solve(dateRange, null, emptyBookings);
            });
        }

        [Fact]
        public void Solve_Given_No_Bookings_Return_Complete_DateRange_as_Available()
        {
            // Arrange 
            DateTime fromDate = DateTime.Today;
            DateTime toDate = fromDate.AddDays(10);
            var dateRange = new DateRange(fromDate, toDate);
            var emptyBookings = Array.Empty<Booking>();
            var fakeRoom = new Room(Guid.NewGuid(), "test");
            var rooms = new Room[] { fakeRoom };

            var expectedAvailableDateRange = new DateRange(
                fromDate,
                toDate.AddDays(1).AddMilliseconds(-1)
            );

            var availabilitySolver = new RoomsCompleteDaysAvailabilitySolver();

            // Act
            var roomsAvailability = availabilitySolver.Solve(
                dateRange,
                rooms,
                emptyBookings
            );

            // Assert
            Assert.NotEmpty(roomsAvailability);
            Assert.Equal(rooms.Length, roomsAvailability.Count());

            var fakeRoomAvailability = roomsAvailability.First();
            Assert.Equal(fakeRoom.Id, fakeRoomAvailability.RoomId);
            Assert.Single(fakeRoomAvailability.AvailableDateRanges);
            Assert.Equal(expectedAvailableDateRange, fakeRoomAvailability.AvailableDateRanges.First());
        }

        [Fact]
        public void Solve_Given_DateRange_Completely_Booked_Returns_EmptyAvailability()
        {
            // Arrange 
            DateTime queryFromDate = DateTime.Today;
            DateTime queryToDate = queryFromDate.AddDays(10);
            var dateRange = new DateRange(queryFromDate, queryToDate);
            var fakeRoom = new Room(Guid.NewGuid(), "test");

            var fakeBookings = new Booking[]{
                new Booking() {
                    Id = Guid.NewGuid(),
                    RoomId = fakeRoom.Id,
                    CustomerId = Guid.NewGuid(),
                    StartDate = queryFromDate.Date,
                    EndDate = queryToDate.Date
                }
            };

            var rooms = new Room[] { fakeRoom };

            var availabilitySolver = new RoomsCompleteDaysAvailabilitySolver();

            // Act
            var roomsAvailability = availabilitySolver.Solve(
                dateRange,
                rooms,
                fakeBookings
            );

            // Assert
            Assert.NotEmpty(roomsAvailability);
            var fakeRoomAvailability = roomsAvailability.First();
            Assert.Empty(fakeRoomAvailability.AvailableDateRanges);
        }

        [Fact]
        public void Solve_Given_Booking_That_Started_Prior_DateRange_Availability_Starts_After()
        {
            // Arrange 
            DateTime queryFromDate = DateTime.Today;
            DateTime queryToDate = queryFromDate.AddDays(10);
            var dateRange = new DateRange(queryFromDate, queryToDate);
            var fakeRoom = new Room(Guid.NewGuid(), "test");

            DateTime fakeBookingStartDate = queryFromDate.AddDays(-1);
            DateTime fakeBookingEndDate = queryFromDate.AddDays(2);

            var fakeBookings = new Booking[]{
                new Booking() {
                    Id = Guid.NewGuid(),
                    RoomId = fakeRoom.Id,
                    CustomerId = Guid.NewGuid(),
                    StartDate = fakeBookingStartDate,
                    EndDate =fakeBookingEndDate
                }
            };

            var rooms = new Room[] { fakeRoom };

            var availabilitySolver = new RoomsCompleteDaysAvailabilitySolver();

            // Act
            var roomsAvailability = availabilitySolver.Solve(
                dateRange,
                rooms,
                fakeBookings
            );

            // Assert
            Assert.NotEmpty(roomsAvailability);
            var fakeRoomAvailability = roomsAvailability.First();
            Assert.Single(fakeRoomAvailability.AvailableDateRanges);
            var fakeRoomAvailableDateRange = fakeRoomAvailability.AvailableDateRanges.First();
            Assert.Equal(
                fakeBookingEndDate.AddDays(1),
                fakeRoomAvailableDateRange.From
            );
             Assert.Equal(
                queryToDate.AddDays(1).AddMilliseconds(-1),
                fakeRoomAvailableDateRange.To
            );
        }

        [Fact]
        public void Solve_Given_Multiple_Booking_Returns_CorrectAvailability()
        {
            // Arrange 
            DateTime queryFromDate = new DateTime(2021, 06, 01);
            DateTime queryToDate = new DateTime(2021, 06, 11);
            var dateRange = new DateRange(queryFromDate, queryToDate);
            var fakeRoom = new Room(Guid.NewGuid(), "test");

            DateTime firstBookingStartDate = new DateTime(2021, 05, 28);
            DateTime firstBookingEndDate = new DateTime(2021, 06, 02);

            DateTime secondBookingStartDate = new DateTime(2021, 06, 04);
            DateTime secondBookingEndDate = new DateTime(2021, 06, 04);

            DateTime thirdBookingStartDate = new DateTime(2021, 06, 08);
            DateTime thirdBookingEndDate = new DateTime(2021, 06, 12);

            var firstExpectedAvailableDateRange = new DateRange(
                firstBookingEndDate.AddDays(1),
                secondBookingStartDate.AddMilliseconds(-1)
            );

            var secondExpectedAvailableDateRange = new DateRange(
                secondBookingEndDate.AddDays(1),
                thirdBookingStartDate.AddMilliseconds(-1)
            );

            var fakeBookings = new Booking[]{
                new Booking() {
                    Id = Guid.NewGuid(),
                    RoomId = fakeRoom.Id,
                    CustomerId = Guid.NewGuid(),
                    StartDate = firstBookingStartDate,
                    EndDate = firstBookingEndDate
                },
                  new Booking() {
                    Id = Guid.NewGuid(),
                    RoomId = fakeRoom.Id,
                    CustomerId = Guid.NewGuid(),
                    StartDate = secondBookingStartDate,
                    EndDate = secondBookingEndDate
                },
                  new Booking() {
                    Id = Guid.NewGuid(),
                    RoomId = fakeRoom.Id,
                    CustomerId = Guid.NewGuid(),
                    StartDate = thirdBookingStartDate,
                    EndDate = thirdBookingEndDate
                }
            };

            var rooms = new Room[] { fakeRoom };

            var availabilitySolver = new RoomsCompleteDaysAvailabilitySolver();

            // Act
            var roomsAvailability = availabilitySolver.Solve(
                dateRange,
                rooms,
                fakeBookings
            );

            // Assert
            Assert.NotEmpty(roomsAvailability);
            var fakeRoomAvailability = roomsAvailability.First();
            Assert.Equal(2, fakeRoomAvailability.AvailableDateRanges.Count());
            Assert.Collection(fakeRoomAvailability.AvailableDateRanges,
               item => Assert.Equal(firstExpectedAvailableDateRange, item),
               item => Assert.Equal(secondExpectedAvailableDateRange, item)
            );
        }
    }
}