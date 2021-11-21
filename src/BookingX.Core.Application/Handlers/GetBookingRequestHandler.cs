using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BookingX.Core.Application.Dtos;
using BookingX.Core.Application.Requests;
using BookingX.Core.Domain.Interfaces;
using BookingX.Core.Domain;
using MediatR;

namespace BookingX.Core.Application.Handlers
{
    public class GetBookingRequestHandler : IRequestHandler<GetBookingRequest, BookingDto>
    {
        private readonly IMapper _mapper;
        private readonly IBookingRepository _bookingRepository;

        public GetBookingRequestHandler(IMapper mapper, IBookingRepository bookingRepository)
        {
            _mapper = mapper;
            _bookingRepository = bookingRepository;
        }
        public async Task<BookingDto> Handle(GetBookingRequest request, CancellationToken cancellationToken)
        {
            if(request ==  null)
                throw new ArgumentNullException(nameof(request));

            Booking booking = await _bookingRepository.GetByIdAsync(request.Id);
            BookingDto bookingDto = _mapper.Map<BookingDto>(booking);

            return bookingDto;
        }
    }
}