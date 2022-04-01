using RabbitMQ.Client;

namespace RabbitMQConsumer
{
    public class Consumer
    {
        private QueueConsumer _queueCreator;

        public Consumer(string queueName, IConnection connection)
        {
            _queueCreator = new QueueConsumer(queueName, connection);
        }

        public void ConsumeMessage()
        {
            _queueCreator.ConsumeMessage();
        }
    }
}