using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BookingX.Core.Application.Requests;
using BookingX.Core.Domain;
using BookingX.Core.Domain.Interfaces;
using MediatR;

namespace BookingX.Core.Application.Handlers
{
    public class UpdateBookingRequestHandler : IRequestHandler<UpdateBookingRequest, bool>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;

        public UpdateBookingRequestHandler(
            IBookingRepository bookingRepository,
            IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }
        public async Task<bool> Handle(UpdateBookingRequest request, CancellationToken cancellationToken)
        {
            var booking = _mapper.Map<Booking>(request.Booking);
            return await _bookingRepository.UpdateAsync(booking);
        }
    }
}