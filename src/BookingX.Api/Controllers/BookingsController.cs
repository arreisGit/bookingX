using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingX.Core.Application.Commands;
using BookingX.Core.Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingX.Api.Controllers
{
    public class BookingsController : BaseApiController
    {
        public BookingsController(IMediator mediator) : base(mediator)
        {
        }

        // TODO: Implement Create Booking
        [HttpGet("{id}")]
        public Task<ActionResult<BookingDto>> GetBooking(Guid id)
        {
            throw new NotImplementedException();
        }

        // TODO: Add ModelBinding validation, passing any body gives default properties values.
        // TODO: Add Swagger example
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

        // TODO: Implement Edit Booking
        [HttpPut("{id}")]
        public Task<IActionResult> Edit(Guid id, BookingDto activity)
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