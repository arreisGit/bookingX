using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookingX.Core.Application.Dtos;
using BookingX.Core.Application.Interfaces;
using BookingX.Core.Application.Requests;
using BookingX.Core.Domain.Interfaces;
using BookingX.Core.Domain.ValueObjects;
using MediatR;

namespace BookingX.Core.Application.Handlers
{

    /// <summary>
    /// GetRoomsAvailabilityQueryHandler class/
    /// </summary>
    public class GetRoomsAvailabilityRequestHandler : IRequestHandler<GetRoomsAvailabilityRequest, IEnumerable<RoomAvailabilityDto>>
    {
        /// <summary>
        /// The room repository.
        /// </summary>
        private readonly IRoomRepository _roomRepository;
        /// <summary>
        /// The booking repository.
        /// </summary>
        private readonly IBookingRepository _bookingRepository;
        private readonly IRoomsAvailabilitySolverStrategy _roomsAvailabilityResolver;


        /// <summary>
        /// Initializes a new instance of <see cref="GetRoomsAvailabilityRequest"/> class.
        /// </summary>
        /// <param name="roomRepository">A room repository.</param>
        /// <param name="bookingRepository">A booking repository.</param>
        public GetRoomsAvailabilityRequestHandler(
            IRoomRepository roomRepository,
            IBookingRepository bookingRepository,
            IRoomsAvailabilitySolverStrategy roomsAvailabilitySolver)
        {
            _roomRepository = roomRepository;
            _bookingRepository = bookingRepository;
            _roomsAvailabilityResolver = roomsAvailabilitySolver;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<RoomAvailabilityDto>> Handle(GetRoomsAvailabilityRequest request, CancellationToken cancellationToken)
        {
            var rooms = await _roomRepository.GetAllRoomsAsync();

            if (rooms.Any())
            {
                var dateRange = new DateRange(request.FromDate, request.ToDate);
                var bookings = await _bookingRepository.GetAllBookingsInDateRange(dateRange);

                IEnumerable<RoomAvailabilityDto> roomsAvailability = _roomsAvailabilityResolver.Solve(dateRange, rooms, bookings);

                return roomsAvailability;
            }

            return null;
        }
    }
}