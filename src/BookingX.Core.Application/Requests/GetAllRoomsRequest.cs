using System.Collections.Generic;
using BookingX.Core.Application.Dtos;
using MediatR;

namespace BookingX.Core.Application.Requests
{
    public class GetAllRoomsRequest : IRequest<ICollection<RoomDto>> { }
}