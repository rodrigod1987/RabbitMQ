using RabbitMQ.Client;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace RabbitMQProducer
{
    internal class QueueCreatorProducer
    {
        public QueueCreatorProducer(string url, string queueName)
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

            var messageStream = ToStream(message);

            Channel.BasicPublish(exchange: "",
                routingKey: QueueName,
                basicProperties: props,
                body: messageStream);
        }

        private byte[] ToStream<T>(T message)
        {
            if (message == null)
            {
                return null;
            }

            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, message);

            return ms.ToArray();
        }
    }
}