using System;

namespace RabbitMQConsumer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var queueName = "MyFirstQueueTask";
            
            //for (int i = 0; i < 2; i++)
            //{
                var consumer = new Consumer(queueName);
                consumer.ConsumeMessage();
            //}
            
            Console.ReadLine();
        }
    }
}
