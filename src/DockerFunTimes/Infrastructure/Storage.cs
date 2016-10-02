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

        public async Task Write(ITableEntity entity)
        {
            var table = _cloudStorageClient.GetTableReference(entity.GetType().Name);
            await table.CreateIfNotExistsAsync();

            await table.ExecuteAsync(TableOperation.Insert(entity));
        }
    }
}