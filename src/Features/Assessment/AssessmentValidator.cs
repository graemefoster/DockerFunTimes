
using System.Collections.Generic;

namespace DDDPerth.Features.Assessment
{
    public class RequestValidator : IValidate<AssessmentRequest>
    {
        public IEnumerable<ValidationFault> Validate(AssessmentRequest request)
        {
            if (request.Name == "Graeme") yield return new ValidationFault("Not you again!");
        }
    }
}