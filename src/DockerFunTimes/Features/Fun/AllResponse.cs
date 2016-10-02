using System.Collections.Generic;

namespace DockerFunTimes.Features.Fun
{
    public class AllResponse
    {
        public IEnumerable<FunRequest> Requests { get; set; }
    }
}