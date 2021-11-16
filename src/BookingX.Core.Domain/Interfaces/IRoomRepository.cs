using System.Collections.Generic;
using System.Threading.Tasks;
using BookingX.Core.Domain;

namespace BookingX.Core.Domain.Interfaces
{
    public interface IRoomRepository
    {
        Task<ICollection<Room>> GetAllRooms();
    }
}