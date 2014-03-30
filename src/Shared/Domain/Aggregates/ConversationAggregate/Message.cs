using System;

namespace Domain.Aggregates.ConversationAggregate
{
    [Serializable]
    public class Message
    {
        protected Message()
        {
            
        }

        public Message(Guid contactId, string content, DateTimeOffset createdAt)
        {
            ContactId = contactId;
            Content = content;
            CreatedAt = createdAt;
        }

        public Guid ContactId { get; protected set; }

        public string Content { get; protected set; }

        public DateTimeOffset CreatedAt { get; protected set; }
    }
}