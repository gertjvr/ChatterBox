using System;

namespace ChatterBox.Domain.Aggregates.UserAggregate
{
    public class PrivateMessage
    {
        public PrivateMessage(string content, Guid userId, DateTimeOffset receivedAt)
        {
            Content = content;
            UserId = userId;
            ReceivedAt = receivedAt;
        }

        public string Content { get; protected set; }

        public Guid UserId { get; protected set; }
        
        public DateTimeOffset ReceivedAt { get; protected set; }
    }
}