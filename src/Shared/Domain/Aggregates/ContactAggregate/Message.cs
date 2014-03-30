using System;

namespace Domain.Aggregates.ContactAggregate
{
    public class Message
    {
        public Message(Guid receiverId, string body)
        {
            Body = body;
            ReceiverId = receiverId;
        }

        public Guid ReceiverId { get; private set; }

        public string Body { get; private set; }
    }
}