using System;

namespace RabbitMQModels
{
    [Serializable]
    public class Message
    {
        public Message(long count)
        {
            Id = count;
        }

        public long Id { get; }
        public string Title { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return $"ID: {Id}, Title: {Title}, Description: {Description}";
        }
    }
}
