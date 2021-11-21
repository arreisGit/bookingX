using System.Threading;
using System.Threading.Tasks;
using BookingX.Core.Application.Requests;
using BookingX.Core.Domain.Interfaces;
using MediatR;

namespace BookingX.Core.Application.Handlers
{
    public class DeleteBookingRequestHandler : IRequestHandler<DeleteBookingRequest, bool>
    {
        private readonly IBookingRepository _bookingRepository;
        public DeleteBookingRequestHandler(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<bool> Handle(DeleteBookingRequest request, CancellationToken cancellationToken)
        {
            return await _bookingRepository.DeleteAsync(request.Id);
        }
    }
}