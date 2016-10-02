using Microsoft.WindowsAzure.Storage.Table;

namespace DockerFunTimes.Features.Fun
{
    public class FooEntity : TableEntity
    {
        public int Number1 { get; set; }
        public int Number2 { get; set; }
    }
}