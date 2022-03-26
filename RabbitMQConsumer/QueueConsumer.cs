using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQModels;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace RabbitMQConsumer
{
    internal class QueueConsumer
    {
        public QueueConsumer(string url, string queueName)
        {
            Url = url;
            QueueName = queueName;

            CreateChannel();
        }

        public string Url { get; }
        public string QueueName { get; }
        public IModel Channel { get; private set; }

        private void CreateChannel()
        {
            var factory = new ConnectionFactory() { Uri = new Uri(Url) };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.QueueDeclare(queue: QueueName, 
                durable: true, 
                exclusive: false, 
                autoDelete: false, 
                arguments: null);

            channel.BasicQos(prefetchSize: 0, 
                prefetchCount: 100, 
                global: false);

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

            Console.WriteLine(" [x] Received {0}", message.ToString());
            //Task.Delay(100).GetAwaiter().GetResult();

            // Note: it is possible to access the channel via
            //       ((EventingBasicConsumer)sender).Model here
            Channel.BasicAck(deliveryTag: eventArgs.DeliveryTag, multiple: false);
        }
    }
}