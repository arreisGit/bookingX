using System;
using BookingX.Core.Domain.Exceptions;
using Xunit;

namespace BookingX.Core.Domain.Tests
{
    public class RoomTests
    {
        [Fact]
        public void Given_EmptyGuid_Throws_InvalidEntityIdException()
        {
            // AAA
            Assert.Throws<InvalidEntityIdException>(() => new Room(Guid.Empty, "101"));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("     ")]
        public void Given_NullEmptyOrWhitespaced_RoomNumber_Throws_StringNullEmptyOrWhitespaceException(string roomNumber)
        {
            // AAA
            Assert.Throws<StringNullEmptyOrWhitespaceException>(() => new Room(Guid.NewGuid(), roomNumber));
        }

    }
}
