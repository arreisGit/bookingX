using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingX.Core.Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookingX.Api.Controllers
{
    public class BookingsController : BaseApiController
    {
        public BookingsController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> Details(Guid id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Booking booking)
        {
            throw new NotImplementedException();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(Guid id, Booking activity)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}