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

        // TODO: Implement Edit Booking
        [HttpPut("{id}")]
        public Task<IActionResult> Update(Guid id, BookingDto activity)
        {
            //BadReqiest. Accepted or NOContent
    

            throw new NotImplementedException();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleteCommand = new DeleteBookingCommand(id);
            var deleted = await _mediator.Send(deleteCommand);
            return deleted ? Accepted() : NotFound();
        }
    }
}