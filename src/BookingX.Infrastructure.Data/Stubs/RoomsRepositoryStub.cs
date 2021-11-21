using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookingX.Core.Domain;
using BookingX.Core.Domain.Interfaces;

namespace BookingX.Infrastructure.Data.Stubs
{
    public class RoomRepositoryStub : IRoomRepository
    {
        private readonly List<Room> _fakeRooms;

        public RoomRepositoryStub()
        {
            
            _fakeRooms = new List<Room>();
            _fakeRooms.Add(new Room(Guid.Parse("8f414952-b2d8-4193-b956-56b33698c7f2"), "101"));
        } 
        public Task<ICollection<Room>> GetAllRoomsAsync()
        {
            return Task.FromResult((ICollection<Room>)_fakeRooms);
        }
    }
}