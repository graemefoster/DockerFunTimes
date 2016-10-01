
using System.Collections.Generic;
using DDDPerth.Features.Assessment;

namespace DockerFunTimes.Features.Assessment
{
    public class RequestValidator : IValidate<AssessmentRequest>
    {
        public IEnumerable<ValidationFault> Validate(AssessmentRequest request)
        {
            if (request.Name == "Graeme") yield return new ValidationFault("Not you again!");
        }
    }
}