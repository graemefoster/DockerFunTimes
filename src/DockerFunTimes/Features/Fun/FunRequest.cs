using MediatR;

namespace DockerFunTimes.Features.Fun
{
    public class FunRequest: IAsyncRequest<FunResponse>
    {
        public int NumberOne { get; set; }
        public int NumberTwo { get; set; }
    }
}