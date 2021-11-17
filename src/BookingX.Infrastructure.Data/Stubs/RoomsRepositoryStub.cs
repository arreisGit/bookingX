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
            _fakeRooms.Add(new Room(Guid.Parse("2c3bd9ef-4747-462e-8ad4-39f4af475a9a"), "101"));
        } 
        public Task<ICollection<Room>> GetAllRooms()
        {
            throw new NotImplementedException();
        }
    }
}