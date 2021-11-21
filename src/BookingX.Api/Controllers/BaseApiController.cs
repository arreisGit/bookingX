using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookingX.Api.Controllers
{

    /// <summary>
    /// An abstracte BaseApi Controller to procure resuability.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public abstract class BaseApiController : ControllerBase
    {
        protected readonly IMediator _mediator;
 
        public BaseApiController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}