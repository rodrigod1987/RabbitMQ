using RabbitMQModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RabbitMQProducer
{
    internal class Program
    {

        static async void Main(string[] args)
        {
            var countDown = 4000;
            var queueName = "MyFirstQueueTask";
            var publisher = new Publisher(queueName);

            var internalCount = 0;
            while (true)
            {
                if (internalCount == 150)
                {
                    await Task.Delay(1000);
                }

                var message = CreateMessage();

                publisher.SendMessage(message);
                countDown--;

                Console.WriteLine("Mensagem produzida: " + message.ToString());
                internalCount++;
            }
        }

        private static Message CreateMessage()
        {
            return new Message() { Description = "Minha descrição", Title = "Meu título" };
        }
    }
}
