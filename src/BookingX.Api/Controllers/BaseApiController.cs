using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookingX.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseApiController : ControllerBase
    {
        protected readonly IMediator _mediator;
 
        public BaseApiController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}