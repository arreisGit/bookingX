using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using BookingX.Core.Application.Dtos;
using BookingX.Core.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookingX.Api.Controllers
{
    /// <summary>
    /// Rooms controller.
    /// </summary>
    public class RoomsController : BaseApiController
    {


        /// <summary>
        /// Initializes a new instance of <see cref="RoomController"/> class.
        /// </summary>
        /// <param name="mediator">Mediator.</param>
        public RoomsController(IMediator mediator) : base(mediator)
        { }


        /// <summary>
        /// Get all the rooms.
        /// </summary>
        /// <returns>A collection of all the rooms registered.</returns>
        [HttpGet]
        public async Task<ActionResult<ICollection<RoomDto>>> GetAllRooms()
        {
            var rooms = await _mediator.Send(new GetAllRoomsQuery());
            return Ok(rooms);
        }

        /// <summary>
        /// Returns all the rooms the availability between an specific date range.
        /// </summary>
        /// <param name="fromDate">The beginning of the date range period to query..</param>
        /// <param name="toDate">The end of the date range period to query.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet("availability")]
        public async Task<IActionResult> GetRoomsAvailability([Required] DateTime? fromDate, [Required] DateTime? toDate)
        {
            var query = new GetRoomsAvailabilityQuery((DateTime)fromDate, (DateTime)toDate);
            var roomsAvailability = await _mediator.Send(query);
            return Ok(roomsAvailability);
        }
    }
}