using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DockerFunTimes.Features.Fun
{
    public class FunApiController : Controller
    {
        private readonly IMediator _mediator;

        public FunApiController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("api/add/{NumberOne}/{NumberTwo}")]
        public FunResponse Get(FunRequest request) {
            return _mediator.Send(request);
        }
    }
}