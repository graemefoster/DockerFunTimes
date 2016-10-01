
using MediatR;

namespace DockerFunTimes.Features.Assessment
{
    public class AssessmentRequest: IRequest<AssessmentResponse> {
        public string Name {get;set;}
    }
}