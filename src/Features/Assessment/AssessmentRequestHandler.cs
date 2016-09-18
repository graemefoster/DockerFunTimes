using System.Collections.Generic;

namespace DDDPerth.Features.Assessment
{
    public class AssessmentRequestHandler : MediatR.IRequestHandler<AssessmentRequest, AssessmentResponse>
    {
        public AssessmentResponse Handle(AssessmentRequest message)
        {
            return new AssessmentResponse {
                Greeting = "Hello " + message.Name
            };
        }
    }}