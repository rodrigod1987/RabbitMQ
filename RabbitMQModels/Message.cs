using System;

namespace RabbitMQModels
{
    [Serializable]
    public class Message
    {
        public Message()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; }
        public string Title { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return $"ID: {Id}, Title: {Title}, Description: {Description}";
        }
    }
}
