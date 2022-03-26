namespace RabbitMQConsumer
{
    public class Consumer
    {
        private QueueConsumer _queueCreator;

        public Consumer(string queueName)
        {
            _queueCreator = new QueueConsumer("amqp://guest:guest@localhost:5672/", queueName);
        }

        public void ConsumeMessage()
        {
            _queueCreator.ConsumeMessage();
        }
    }
}