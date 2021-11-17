using System.Collections.Generic;
using BookingX.Core.Application.Dtos;
using MediatR;

namespace BookingX.Core.Application.Queries
{
    public class GetAllRoomsQuery : IRequest<ICollection<Room>> { }
}