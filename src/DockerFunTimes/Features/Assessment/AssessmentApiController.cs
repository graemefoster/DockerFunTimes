using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DockerFunTimes.Features.Assessment
{
    public class AssessmentApiController : Controller
    {
        private readonly IMediator _mediator;

        public AssessmentApiController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("api/assessment/{Name}")]
        public AssessmentResponse Get(AssessmentRequest request) {
            return _mediator.Send(request);
        }
    }


}