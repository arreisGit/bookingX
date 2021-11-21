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

        // TODO: EndDate cannot be more than 3 days of difference than StartDate
        // TODO: StartDate cannot be more than 30 days in advance
        // TODO: StartDate must needs to start, at least next day after booking (creation)
        public async Task<BookingDto> Handle(CreateBookingRequest request, CancellationToken cancellationToken)
        {
            var booking = _mapper.Map<Booking>(request.BookingDto);
            var insertedBooking = await _bookingRepository.CreateAsync(booking);
            var insertedBookingDto = _mapper.Map<BookingDto>(insertedBooking);
            return insertedBookingDto;
        }
    }
}