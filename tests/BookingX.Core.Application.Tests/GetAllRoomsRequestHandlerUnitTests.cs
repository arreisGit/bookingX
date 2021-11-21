using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using BookingX.Core.Domain;
using BookingX.Core.Domain.Interfaces;
using BookingX.Core.Application.Handlers;
using BookingX.Core.Application.Requests;
using BookingX.Core.Application.Tests.ClassFixtures;
using Xunit;
using NSubstitute;
using AutoMapper;

namespace BookingX.Core.Application.Tests
{
    public class GetAllRoomsRequestHandlerUnitTests : IClassFixture<AutoMapperFixture>
    {
        private readonly IMapper _mapper;

        public GetAllRoomsRequestHandlerUnitTests(AutoMapperFixture automapperFixture)
        {
            _mapper = automapperFixture.Mapper;
        }

        [Fact]
        public async Task Handle_Returns_All_Rooms()
        {
            // Arrange
            var stubbedRooms = new List<Room>();
            stubbedRooms.Add(new Room(Guid.NewGuid(), "101"));
            stubbedRooms.Add(new Room(Guid.NewGuid(), "102"));
            stubbedRooms.Add(new Room(Guid.NewGuid(), "103"));

            var roomsRepositoryStub = Substitute.For<IRoomRepository>();
            roomsRepositoryStub
                .GetAllRoomsAsync()
                .ReturnsForAnyArgs(Task.FromResult((ICollection<Room>)stubbedRooms));

            var Request = new GetAllRoomsRequest();
            var handler = new GetAllRoomsRequestHandler(_mapper, roomsRepositoryStub);

            // Act
            var returnedRooms = await handler
                            .Handle(Request, CancellationToken.None)
                            .ConfigureAwait(false);

            // Assert
            Assert.NotNull(returnedRooms);
            Assert.Equal(stubbedRooms.Count, returnedRooms.Count);
        }
    }
}
