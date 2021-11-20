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
            
        // TODO: EndDate cannot be more than 3 days of difference than StartDate
        // TODO: StartDate cannot be more than 30 days in advance
        // TODO: StartDate must needs to start, at least next day after booking (creation)
        public async Task<Unit> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            var booking = _mapper.Map<Booking>(request.BookingDto);
            await _bookingRepository.CreateAsync(booking);
            return Unit.Value;
        }
    }
}