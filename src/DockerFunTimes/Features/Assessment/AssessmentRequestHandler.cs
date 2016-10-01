using MediatR;

namespace DockerFunTimes.Features.Assessment
{
    public class AssessmentRequestHandler : IRequestHandler<AssessmentRequest, AssessmentResponse>
    {
        public AssessmentResponse Handle(AssessmentRequest message)
        {
            return new AssessmentResponse {
                Greeting = "Hello " + message.Name
            };
        }
    }}