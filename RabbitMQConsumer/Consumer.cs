namespace RabbitMQConsumer
{
    public class Consumer
    {
        private QueueCreatorConsumer _queueCreator;

        public Consumer(string queueName)
        {
            _queueCreator = new QueueCreatorConsumer("amqp://guest:guest@localhost:5672/", queueName);
        }

        public void ConsumeMessage()
        {
            _queueCreator.ConsumeMessage();
        }
    }
}