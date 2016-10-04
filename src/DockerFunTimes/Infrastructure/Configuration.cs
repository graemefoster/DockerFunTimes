namespace DockerFunTimes.Infrastructure
{
    public class Configuration
    {
        public string BlobStoragePassword { get; set; }
        public bool UseRabbit { get; set; }
        public string RabbitConnection { get; set; }
    }
}