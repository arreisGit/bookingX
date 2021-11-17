using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using BookingX.Core.Domain;
using BookingX.Core.Domain.Interfaces;
using BookingX.Core.Application.Handlers;
using BookingX.Core.Application.Queries;
using Xunit;
using NSubstitute;

namespace BookingX.Core.Application.Tests
{
    public class GetAllRoomsQueryHandlerUnitTests
    {
        [Fact]
        public async Task Given_Good_GetAllQuery_Return_All_Rooms()
        {
            // Arrange
            var stubbedRooms = new List<Room>();
            stubbedRooms.Add(new Room(Guid.NewGuid(), "101"));
            stubbedRooms.Add(new Room(Guid.NewGuid(), "102"));
            stubbedRooms.Add(new Room(Guid.NewGuid(), "103"));

            var roomsRepositoryStub = Substitute.For<IRoomRepository>();
            roomsRepositoryStub
                .GetAllRooms()
                .ReturnsForAnyArgs(Task.FromResult((ICollection<Room>)stubbedRooms));

            var query = new GetAllRoomsQuery();
            var handler = new GetAllRoomsQueryHandler(roomsRepositoryStub);

            // Act
            var returnedRooms = await handler
                            .Handle(query, CancellationToken.None)
                            .ConfigureAwait(false);

            // Assert
            Assert.NotNull(returnedRooms);
            Assert.Equal(stubbedRooms.Count, returnedRooms.Count);
        }
    }
}
