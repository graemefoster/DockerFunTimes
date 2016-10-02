using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

namespace DockerFunTimes.Infrastructure
{
    public interface IStorage
    {
        Task Write(ITableEntity entity);
    }
}