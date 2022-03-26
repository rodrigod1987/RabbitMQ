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
            var publisher = new Publisher(queueName);

            while (true)
            {
                var message = CreateMessage(count);

                publisher.SendMessage(message);
                count++;

                Console.WriteLine("Mensagem produzida: " + message.ToString());
            }
        }

        private static Message CreateMessage(int count)
        {
            return new Message() { Description = "Minha descrição " + count, Title = "Meu título " + count };
        }
    }
}
