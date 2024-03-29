using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BookingX.Core.Application.Dtos;
using BookingX.Core.Application.Requests;
using BookingX.Core.Domain;
using BookingX.Core.Domain.Interfaces;
using MediatR;

namespace BookingX.Core.Application.Handlers
{
    public class CreateBookingRequestHandler : IRequestHandler<CreateBookingRequest, BookingDto>
    {
        private readonly IMapper _mapper;
        private readonly IBookingRepository _bookingRepository;

        public CreateBookingRequestHandler(
            IMapper mapper,
            IBookingRepository bookingRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _bookingRepository = bookingRepository ?? throw new ArgumentNullException(nameof(bookingRepository));
        }

        public async Task<BookingDto> Handle(CreateBookingRequest request, CancellationToken cancellationToken)
        {
            var booking = _mapper.Map<Booking>(request.Booking);
            var insertedBooking = await _bookingRepository.CreateAsync(booking);
            var insertedBookingDto = _mapper.Map<BookingDto>(insertedBooking);
            return insertedBookingDto;
        }
    }
}