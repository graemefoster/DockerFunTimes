using System;
using System.Text;
using System.Threading;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace DockerFunRabbitReader
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var config = new ConfigurationSettings();
                var connectionFactory = new ConnectionFactory() {Uri = config.Configuration.RabbitConnection};

                bool connected = false;
                while (!connected)
                {
                    try
                    {
                        Subscribe(connectionFactory);
                        connected = true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Failed to connect to 'da Rabbit :) - " + ex.Message);
                        Thread.Sleep(5000);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("We are listening!!");
        }

        private static void Subscribe(IConnectionFactory connectionFactory)
        {
            using (var client = connectionFactory.CreateConnection())
            using (var channel = client.CreateModel())
            {
                var queue = channel.QueueDeclare("TestSubscriptionToGraemesTestQueue",
                    true, true, true, null);

                channel.QueueBind("TestSubscriptionToGraemesTestQueue", "GraemesFanoutExchange",
                    "", null);

                var sub = new EventingBasicConsumer(channel);
                sub.Registered += (sender, eventArgs) =>
                {
                    Console.WriteLine("Registered - " + eventArgs.ConsumerTag);
                };

                sub.Received += (sender, eventArgs) =>
                {
                    if (eventArgs.Body != null)
                    {
                        Console.WriteLine("Holy cow - " + Encoding.UTF8.GetString(eventArgs.Body));
                    }
                    else
                    {
                        Console.WriteLine("Hmm received an empty message body...");
                        Console.WriteLine(eventArgs);
                    }
                };

                Console.WriteLine("Calling basic consume");

                var whoknows = channel.BasicConsume("TestSubscriptionToGraemesTestQueue", false, sub);

                Console.WriteLine("Listening for messages....!!!");

                while (true)
                {
                    Thread.Sleep(100);
                }
            }
        }
    }
}


