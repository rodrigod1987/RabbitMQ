using System;

namespace RabbitMQProducer
{
    public class Publisher
    {
        private QueueCreatorProducer _queueCreator;

        public Publisher(string queueName)
        {
            _queueCreator = new QueueCreatorProducer("amqp://guest:guest@localhost:5672/", queueName);
        }

        public void SendMessage<T>(T message) where T : class
        {
            _queueCreator.SendMessage(message);
        }
    }
}