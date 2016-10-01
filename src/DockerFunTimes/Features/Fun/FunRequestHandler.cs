using MediatR;

namespace DockerFunTimes.Features.Fun
{
    public class FunRequestHandler : IRequestHandler<FunRequest, FunResponse>
    {
        public FunResponse Handle(FunRequest message)
        {
            return new FunResponse()
            {
                Sum = message.NumberOne + message.NumberTwo,
                Text =
                    "Thankyou for your request to add the numbers " + message.NumberOne + ", and " + message.NumberTwo
            };
        }
    }
}