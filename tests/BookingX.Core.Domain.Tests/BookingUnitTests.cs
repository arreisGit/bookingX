using System;
using BookingX.Core.Domain.Exceptions;
using Xunit;

namespace BookingX.Core.Domain.Tests
{
    public class BookingTests
    {
        [Fact]
        public void Given_EmptyGuid_Id_Throws_InvalidEntityIdException()
        {
            // AAA
            Assert.Throws<InvalidEntityIdException>(() => new Booking { Id = Guid.Empty });
        }

        [Fact]
        public void Given_EmptyGuid_CustomerId_Throws_InvalidEntityIdException()
        {
            // AAA
            Assert.Throws<InvalidEntityIdException>(() => new Booking { CustomerId = Guid.Empty });
        }

        [Fact]
        public void Given_EmptyGuid_RoomId_Throws_InvalidEntityIdException()
        {
            // AAA
            Assert.Throws<InvalidEntityIdException>(() => new Booking { RoomId = Guid.Empty });
        }

        [Fact]
        public void Given_NonEmptyGuid_Ids_Instance_Gets_Created()
        {
            // Arrange && Act
            var falseBooking = new Booking
            {
                Id = Guid.NewGuid(),
                CustomerId = Guid.NewGuid(),
                RoomId = Guid.NewGuid()
            };

            // Act
            Assert.NotNull(falseBooking);
        }
    }
}