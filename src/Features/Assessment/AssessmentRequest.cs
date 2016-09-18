
namespace DDDPerth.Features.Assessment
{
    public class AssessmentRequest: MediatR.IRequest<AssessmentResponse> {
        public string Name {get;set;}
    }
}