using System.Threading.Tasks;
using DockerFunTimes.Features.Fun;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace DockerFunTimes.Features.Prospects
{
    public class ProspectController: Controller
    {
        private readonly IMediator _mediator;

        public ProspectController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("prospect")]
        public Task<int> Post([FromBody] NewProspectRequest request) {
            return _mediator.SendAsync(request);
        }

        [HttpGet("prospect/{id}")]
        public Task<TerribleEntity> Get(int id)
        {
            return _mediator.SendAsync(new GetProspectRequest(id));
        }
    }
}