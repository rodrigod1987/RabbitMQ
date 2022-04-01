using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RabbitMQConsumer
{
    internal class QueueConsumer
    {
        private static int RANDOM_TIME = new Random(700).Next(1200);
        private readonly IConnection _connection;

        public QueueConsumer(string queueName, IConnection connection)
        {
            QueueName = queueName;
            _connection = connection;

            CreateChannel();
        }

        public string QueueName { get; }
        public IModel Channel { get; private set; }

        private void CreateChannel()
        {
            var channel = _connection.CreateModel();

            channel.QueueDeclare(queue: QueueName, 
                durable: true, 
                exclusive: false, 
                autoDelete: false, 
                arguments: null);

            channel.BasicQos(prefetchSize: 0, 
                prefetchCount: 5, 
                global: true);

            Channel = channel;
        }

        public void ConsumeMessage()
        {
            var consumer = new EventingBasicConsumer(Channel);
            consumer.Received += OnReceived;
            Channel.BasicConsume(queue: QueueName,
                                 autoAck: false,
                                 consumer: consumer);
        }

        private void OnReceived(object sender, BasicDeliverEventArgs eventArgs)
        {
            var message = eventArgs.Body.ToArray().To<Message>();
            Console.WriteLine("[x] Received {0}", message.ToString());

            if (message.Id % 20 == 0)
            {
                Task.Delay(9000).GetAwaiter().GetResult();
            }
            else
            {
                Task.Delay(RANDOM_TIME).GetAwaiter().GetResult();
            }

            Channel.BasicAck(deliveryTag: eventArgs.DeliveryTag, multiple: false);
        }
    }
}