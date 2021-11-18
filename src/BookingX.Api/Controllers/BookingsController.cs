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
    // TODO: Add Swagger example
    public class BookingsController : BaseApiController
    {
        public BookingsController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookingDto>> GetBooking(Guid id)
        {
            var booking = await _mediator.Send(new GetBookingQuery(id));
            if(booking != null)
            {
                return Ok(booking);
            }
            else{
                return NoContent();
            };
        }

        // TODO: Add ModelBinding validation, passing any body gives default properties values.
       
        // Maybe FluentValidation?
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateBooking(BookingDto bookingDto)
        {
            var createBookingCommand = new CreateBookingCommand(bookingDto);
            await _mediator.Send(createBookingCommand);
            return CreatedAtAction(nameof(GetBooking), new {id = bookingDto.Id}, bookingDto);
        }

        // TODO: Implement Etag to control concurrency
        // TODO: Implement Edit Booking
        [HttpPut("{id}")]
        public Task<IActionResult> Update(Guid id, BookingDto activity)
        {
            throw new NotImplementedException();
        }

        // TODO: Implement Delete Booking
        [HttpDelete("{id}")]
        public Task<IActionResult> Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}