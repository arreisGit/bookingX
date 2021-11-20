using System.Threading;
using System.Threading.Tasks;
using BookingX.Core.Application.Commands;
using BookingX.Core.Domain.Interfaces;
using MediatR;

namespace BookingX.Core.Application.Handlers
{
    public class DeleteBookingCommandHandler : IRequestHandler<DeleteBookingCommand, bool>
    {
        private readonly IBookingRepository _bookingRepository;
        public DeleteBookingCommandHandler(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }
        
        public async Task<bool> Handle(DeleteBookingCommand request, CancellationToken cancellationToken)
        {
            return  await _bookingRepository.DeleteAsync(request.Id);
        }
    }
}