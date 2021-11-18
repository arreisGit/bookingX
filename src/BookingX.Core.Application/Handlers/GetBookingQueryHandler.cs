using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BookingX.Core.Application.Dtos;
using BookingX.Core.Application.Queries;
using BookingX.Core.Domain;
using BookingX.Core.Domain.Interfaces;
using MediatR;

namespace BookingX.Core.Application.Handlers
{
    // TODO: We could implement a base class with the Template Pattern to get a nice 
    // forced validation implementation.
    public class GetBookingQueryHandler : IRequestHandler<GetBookingQuery, BookingDto>
    {
        private readonly IMapper _mapper;
        private readonly IBookingRepository _bookingRepository;

        public GetBookingQueryHandler(IMapper mapper, IBookingRepository bookingRepository)
        {
            _mapper = mapper;
            _bookingRepository = bookingRepository;
        }
        public async Task<BookingDto> Handle(GetBookingQuery request, CancellationToken cancellationToken)
        {
            if(request ==  null)
                throw new ArgumentNullException(nameof(request));

            Booking booking = await _bookingRepository.GetById(request.Id);
            BookingDto bookingDto = _mapper.Map<BookingDto>(booking);

            return bookingDto;
        }
    }
}