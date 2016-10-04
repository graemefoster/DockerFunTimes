using System;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace DockerFunTimes.Infrastructure
{
    public class Queue: IQueue
    {
        private readonly ConnectionFactory _connectionFactory;

        public Queue()
        {
            _connectionFactory = new ConnectionFactory() { HostName = "rabbit" };
        }

        public void Publish<TMessage>(TMessage message)
        {
            using (var client = _connectionFactory.CreateConnection())
            using (var channel = client.CreateModel())
            {
                var queue = channel.QueueDeclare("GraemesTestQueue",
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
                channel.BasicPublish(exchange: "",
                    routingKey: "helloWorld",
                    basicProperties: null,
                    body: body);

                Console.WriteLine("Written message to rabbit queue");
            }
        }
    }
}