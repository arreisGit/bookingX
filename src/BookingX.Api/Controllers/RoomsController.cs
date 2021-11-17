using System.Collections.Generic;
using System.Threading.Tasks;
using BookingX.Core.Application.Dtos;
using BookingX.Core.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookingX.Api.Controllers
{
    public class RoomsController : BaseApiController
    {
        public RoomsController(IMediator mediator) : base(mediator)
        { }

        [HttpGet]
        public async Task<ActionResult<ICollection<Room>>> GetAllRooms()
        {
            var rooms = await _mediator.Send(new GetAllRoomsQuery());
            return Ok(rooms);
        }
    }
}