using RabbitMQ.Client;
using RabbitMQModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RabbitMQProducer
{
    internal class QueueProducer
    {
        private IBasicProperties _props;

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

            CreateProperties();
        }

        private void CreateProperties()
        {
            _props = Channel.CreateBasicProperties();
            _props.Persistent = true;
        }

        public void SendMessage<T>(T message)
        {            
            var bytes = message.ToByteArray();

            Task.Delay(3).GetAwaiter().GetResult();

            Channel.BasicPublish(exchange: "",
                routingKey: QueueName,
                basicProperties: _props,
                body: bytes);
        }
    }
}