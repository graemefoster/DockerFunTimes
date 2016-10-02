using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

namespace DockerFunTimes.Infrastructure
{
    public interface IStorage
    {
        Task Write<T>(T entity) where T:ITableEntity;
        Task<IEnumerable<T>> All<T>() where T:ITableEntity, new();
    }
}