using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace DockerFunTimes.Features.Fun
{
    public class FunRequestHandler : IAsyncRequestHandler<FunRequest, FunResponse>
    {
        public async Task<FunResponse> Handle(FunRequest message)
        {
            var foo = Environment.GetEnvironmentVariable("blobstorage");
            var storage = Microsoft.WindowsAzure.Storage.CloudStorageAccount.Parse(
                "DefaultEndpointsProtocol=https;AccountName=graemesdockerfun;AccountKey=" + foo);
            var client = storage.CreateCloudTableClient();
            var table = client.GetTableReference("FooFoo");
            await table.CreateIfNotExistsAsync();

            await table.ExecuteAsync(TableOperation.Insert(new FooEntity()
            {
                Number1 = message.NumberOne,
                Number2 = message.NumberTwo,
                PartitionKey = message.NumberOne.ToString(),
                Timestamp = DateTimeOffset.Now,
                RowKey = Guid.NewGuid().ToString()
            }));

            return new FunResponse()
            {
                Sum = message.NumberOne + message.NumberTwo,
                Text =
                    "Thankyou for your request to add the numbers " + message.NumberOne + ", and " + message.NumberTwo
            };
        }
    }
}