using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DDDPerth.Features.Assessment
{
    public class AssessmentApiController : Microsoft.AspNetCore.Mvc.Controller
    {
        private IMediator _mediator;

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