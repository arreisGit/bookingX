using System;
using System.Threading.Tasks;
using BookingX.Core.Application.Requests;
using BookingX.Core.Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingX.Api.Controllers
{
    /// <summary>
    /// Booking controller.
    /// </summary>
    public class BookingsController : BaseApiController
    {
        /// <summary>
        /// Initializes a new instance of <see cref="BookingsController"/> class.
        /// </summary>
        /// <param name="mediator">Mediator.</param>>
        public BookingsController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// Get a Booking by Id.
        /// </summary>
        /// <param name="id">The Booking Id</param>
        /// <returns>Booking</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BookingDto>> GetBooking(Guid id)
        {
            var getRequest = new GetBookingRequest(id);
            var booking = await _mediator.Send(getRequest);
            if (booking != null)
            {
                return Ok(booking);
            }
            else
            {
                return NotFound();
            };
        }

        /// <summary>
        /// Creates a new booking
        /// </summary>
        /// <param name="bookingDto">The booking to be created</param>
        /// <returns>HTTP 201 the location of the new booking.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateBooking(BookingDto bookingDto)
        {
            var createRequest = new CreateBookingRequest(bookingDto);
            var newBooking = await _mediator.Send(createRequest);
            return CreatedAtAction(nameof(GetBooking), new { id = bookingDto.Id }, newBooking);
        }

        /// <summary>
        /// Updates an already existent booking.
        /// </summary>
        /// <param name="id">The booking Id</param>
        /// <param name="booking">The updated booking details</param>
        /// <returns>HTTP 202 if the update process was successful, HTTP 404 otherwhise</returns>
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, BookingDto booking)
        {
            booking.Id = id.ToString();
            var updateRequest = new UpdateBookingRequest(booking);
            var updated = await _mediator.Send(updateRequest);
            return updated ? Accepted() : NotFound();
        }

        /// <summary>
        /// Deletes a booking.
        /// </summary>
        /// <param name="id">The id from the booking to be deleted.</param>
        /// <returns>HTTP 202 if the deletion was successful, HTTP 404 otherwhise</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleteRequest = new DeleteBookingRequest(id);
            var deleted = await _mediator.Send(deleteRequest);
            return deleted ? Accepted() : NotFound();
        }
    }
}