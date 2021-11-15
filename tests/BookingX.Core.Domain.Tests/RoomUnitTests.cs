using System;
using BookingX.Core.Domain.Exception;
using Xunit;

namespace BookingX.Core.Domain.Tests
{
    public class RoomTests
    {
        [Fact]
        public void Given_EmptyGuid_Throws_InvalidEntityIdException()
        {
            // AAA
            Assert.Throws<InvalidEntityIdException>(() => new Room{ Id = Guid.Empty });
        }

        [Fact]
        public void Given_NonEmptyGuid_Id_Instance_Gets_Created()
        {
            // Arrange && Act
            var falseBooking = new Room
            {
                Id = Guid.NewGuid()
            };

            // Act
            Assert.NotNull(falseBooking);
        }
    }
}
