using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BookingX.Core.Application.Commands;
using BookingX.Core.Domain;
using BookingX.Core.Domain.Interfaces;
using MediatR;

namespace BookingX.Core.Application.Handlers
{
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand>
    {
        private readonly IMapper _mapper;
        private readonly IBookingRepository _bookingRepository;
        
        public CreateBookingCommandHandler(
            IMapper mapper,
            IBookingRepository bookingRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _bookingRepository = bookingRepository ?? throw new ArgumentNullException(nameof(bookingRepository));
        }
            
        public async Task<Unit> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            var booking = _mapper.Map<Booking>(request.BookingDto);
            await _bookingRepository.CreateBooking(booking);
            return Unit.Value;
        }
    }
}