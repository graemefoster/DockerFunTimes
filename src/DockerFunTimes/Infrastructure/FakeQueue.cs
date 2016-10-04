namespace DockerFunTimes.Infrastructure
{
    public class FakeQueue : IQueue
    {
        public void Publish<TMessage>(TMessage message)
        {
        }
    }
}