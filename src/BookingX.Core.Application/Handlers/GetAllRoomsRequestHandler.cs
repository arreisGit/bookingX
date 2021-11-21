using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using BookingX.Core.Application.Requests;
using BookingX.Core.Application.Dtos;
using BookingX.Core.Domain.Interfaces;
using MediatR;
using AutoMapper;

namespace BookingX.Core.Application.Handlers
{
    public class GetAllRoomsRequestHandler
        : IRequestHandler<GetAllRoomsRequest, ICollection<RoomDto>>
    {
        private readonly IRoomRepository _roomsRepository;
        private readonly IMapper _mapper;

        public GetAllRoomsRequestHandler(
            IMapper mapper,
            IRoomRepository roomsRepository)
        {
            _mapper = mapper;
            _roomsRepository = roomsRepository;
        }
        public async Task<ICollection<RoomDto>> Handle(
            GetAllRoomsRequest request, CancellationToken cancellationToken)
        {
            var rooms = await _roomsRepository.GetAllRoomsAsync().ConfigureAwait(false);
            var roomsDtos = _mapper.Map<ICollection<Domain.Room>, ICollection<Dtos.RoomDto>>(rooms);
            return roomsDtos;
        }
    }
}