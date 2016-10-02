using System.Linq;
using System.Threading.Tasks;
using DockerFunTimes.Infrastructure;
using MediatR;

namespace DockerFunTimes.Features.Fun
{
    public class AllRequestHandler : IAsyncRequestHandler<AllRequest, AllResponse>
    {
        private readonly IStorage _storage;

        public AllRequestHandler(IStorage storage)
        {
            _storage = storage;
        }

        public async Task<AllResponse> Handle(AllRequest message)
        {
            return new AllResponse()
            {
                Requests = (await _storage.All<FooEntity>()).Select(x => new FunRequest()
                {
                    NumberOne = x.Number1,
                    NumberTwo = x.Number2
                })
            };
        }
    }
}