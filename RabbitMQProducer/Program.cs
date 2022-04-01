using RabbitMQModels;
using System;
using System.Threading.Tasks;

namespace RabbitMQProducer
{
    internal class Program
    {

        static void Main(string[] args)
        {
            var count = 0;
            var queueName = "MyFirstQueueTask";

            while (true)
            {
                var message = CreateMessage(count);

                var publisher = new Publisher(queueName);
                publisher.SendMessage(message);

                count++;

                Console.WriteLine("[+] Produced: " + message.ToString());
            }
        }

        private static Message CreateMessage(int count)
        {
            return new Message(count) { Description = "Minha descrição " + count, Title = "Meu título " + count };
        }
    }
}
