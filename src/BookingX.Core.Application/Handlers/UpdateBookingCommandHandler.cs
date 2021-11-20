using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BookingX.Core.Application.Commands;
using BookingX.Core.Domain;
using BookingX.Core.Domain.Interfaces;
using MediatR;

namespace BookingX.Core.Application.Handlers
{
    public class UpdateBookingCommandHandler : IRequestHandler<UpdateBookingCommand, bool>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;

        public UpdateBookingCommandHandler(
            IBookingRepository bookingRepository,
            IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }
        public async Task<bool> Handle(UpdateBookingCommand request, CancellationToken cancellationToken)
        {
            var booking = _mapper.Map<Booking>(request.Booking);
            return await _bookingRepository.UpdateAsync(booking);
        }
    }
}