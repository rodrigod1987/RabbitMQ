using System;

namespace RabbitMQProducer
{
    public class Publisher
    {
        private QueueProducer _queueCreator;

        public Publisher(string queueName)
        {
            _queueCreator = new QueueProducer("amqp://guest:guest@localhost:5672/", queueName);
        }

        public void SendMessage<T>(T message) where T : class
        {
            _queueCreator.SendMessage(message);
        }
    }
}