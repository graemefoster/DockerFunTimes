namespace DockerFunTimes.Infrastructure
{
    public interface IQueue
    {
        void Publish<TMessage>(TMessage message);
    }
}