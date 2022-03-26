using RabbitMQ.Client;
using RabbitMQModels;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace RabbitMQProducer
{
    internal class QueueProducer
    {
        public QueueProducer(string url, string queueName)
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

            Channel = channel;
        }

        public void SendMessage<T>(T message)
        {
            var props = Channel.CreateBasicProperties();
            props.Persistent = true;

            var bytes = message.ToByteArray();

            Task.Delay(80).GetAwaiter().GetResult();

            Channel.BasicPublish(exchange: "",
                routingKey: QueueName,
                basicProperties: props,
                body: bytes);
        }
    }
}