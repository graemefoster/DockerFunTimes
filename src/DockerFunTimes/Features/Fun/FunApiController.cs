using System.Collections.Generic;
using System.Threading.Tasks;
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
        public Task<FunResponse> Get(FunRequest request) {
            return _mediator.SendAsync(request);
        }

        [HttpGet("api/add")]
        public Task<AllResponse> All()
        {
            return _mediator.SendAsync(new AllRequest());
        }
    }
}