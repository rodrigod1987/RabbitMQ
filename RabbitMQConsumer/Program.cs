using System;
using System.Threading.Tasks;

namespace RabbitMQConsumer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var queueName = "MyFirstQueueTask";

            var consumer = new Consumer(queueName);
            consumer.ConsumeMessage();

            Console.ReadLine();
        }
    }
}
