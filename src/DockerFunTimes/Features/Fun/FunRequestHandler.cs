using System;
using System.Threading.Tasks;
using DockerFunTimes.Infrastructure;
using MediatR;

namespace DockerFunTimes.Features.Fun
{
    public class FunRequestHandler : IAsyncRequestHandler<FunRequest, FunResponse>
    {
        private readonly IStorage _storage;

        public FunRequestHandler(IStorage storage)
        {
            _storage = storage;
        }

        public async Task<FunResponse> Handle(FunRequest message)
        {
            await _storage.Write(new FooEntity()
            {
                Number1 = message.NumberOne,
                Number2 = message.NumberTwo,
                PartitionKey = message.NumberOne.ToString(),
                Timestamp = DateTimeOffset.Now,
                RowKey = Guid.NewGuid().ToString()
            });

            return new FunResponse()
            {
                Sum = message.NumberOne + message.NumberTwo,
                Text = "Thankyou for your request to add the numbers " + message.NumberOne + ", and " + message.NumberTwo
            };
        }
    }
}