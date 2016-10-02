using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

namespace DockerFunTimes.Infrastructure
{
    public class Storage: IStorage
    {
        private readonly CloudTableClient _cloudStorageClient;

        public Storage(ConfigurationSettings settings)
        {
            _cloudStorageClient = Microsoft.WindowsAzure.Storage.CloudStorageAccount.Parse(
                "DefaultEndpointsProtocol=https;AccountName=graemesdockerfun;AccountKey=" + settings.Configuration.BlobStoragePassword)
                .CreateCloudTableClient();
        }

        public async Task Write<T>(T entity) where T: ITableEntity
        {
            var table = _cloudStorageClient.GetTableReference(typeof(T).Name);
            await table.CreateIfNotExistsAsync();
            await table.ExecuteAsync(TableOperation.Insert(entity));
        }

        public async Task<IEnumerable<T>> All<T>() where T:ITableEntity, new()
        {
            var table = _cloudStorageClient.GetTableReference(typeof(T).Name);
            await table.CreateIfNotExistsAsync();
            var token = new TableContinuationToken();
            var query = new TableQuery<T> {TakeCount = 10};
            var results = await table.ExecuteQuerySegmentedAsync(query, token);
            return results.Results;
        }
    }
}