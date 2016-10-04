using System;
using System.Threading.Tasks;
using DockerFunTimes.Infrastructure;
using MediatR;

namespace DockerFunTimes.Features.Fun
{
    public class FunRequestHandler : IAsyncRequestHandler<FunRequest, FunResponse>
    {
        private readonly IStorage _storage;
        private readonly IQueue _queue;

        public FunRequestHandler(IStorage storage, IQueue queue)
        {
            _storage = storage;
            _queue = queue;
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

            _queue.Publish(message);

            return new FunResponse()
            {
                Sum = message.NumberOne + message.NumberTwo,
                Text = "Thankyou for your request to add the numbers " + message.NumberOne + ", and " + message.NumberTwo
            };
        }
    }
}