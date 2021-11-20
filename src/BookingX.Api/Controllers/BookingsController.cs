using System;
using System.Threading.Tasks;
using BookingX.Core.Application.Commands;
using BookingX.Core.Application.Dtos;
using BookingX.Core.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingX.Api.Controllers
{
    // TODO: Add custom Swagger examples
    // TODO: Add Validations. Use fluent validation pipeline with Mediatr
    public class BookingsController : BaseApiController
    {
        public BookingsController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookingDto>> GetBooking(Guid id)
        {
            var booking = await _mediator.Send(new GetBookingQuery(id));
            if (booking != null)
            {
                return Ok(booking);
            }
            else
            {
                return NotFound();
            };
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateBooking(BookingDto bookingDto)
        {
            var createBookingCommand = new CreateBookingCommand(bookingDto);
            await _mediator.Send(createBookingCommand);
            return CreatedAtAction(nameof(GetBooking), new { id = bookingDto.Id }, bookingDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, BookingDto booking)
        {
            booking.Id = id.ToString();
            var updateBookingCommand = new UpdateBookingCommand(booking);
            var updated = await _mediator.Send(updateBookingCommand);
            return updated ? Accepted() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleteBookingCommand = new DeleteBookingCommand(id);
            var deleted = await _mediator.Send(deleteBookingCommand);
            return deleted ? Accepted() : NotFound();
        }
    }
}