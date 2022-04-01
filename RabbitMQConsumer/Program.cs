using RabbitMQ.Client;
using System;

namespace RabbitMQConsumer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            for (int machineNumber = 0; machineNumber < 4; machineNumber++) 
            { 
                var queueName = "MyFirstQueueTask";
                var factory = new ConnectionFactory() { Uri = new Uri("amqp://guest:guest@localhost:5672/") };
                var connection = factory.CreateConnection();

                for (int consumerNumber = 0; consumerNumber < 25; consumerNumber++)
                {
                    var consumer = new Consumer(queueName, connection);
                    consumer.ConsumeMessage();
                }
            
            }

            Console.ReadLine();
        }
    }
}
