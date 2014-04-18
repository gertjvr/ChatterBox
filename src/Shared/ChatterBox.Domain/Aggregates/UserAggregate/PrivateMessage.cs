using System;
using ChatterBox.Domain.Properties;

namespace ChatterBox.Domain.Aggregates.UserAggregate
{
    public class PrivateMessage
    {
        public PrivateMessage(string content, Guid userId, DateTimeOffset receivedAt)
        {
            if (content == null) 
                throw new ArgumentNullException("content");

            if (userId == Guid.Empty)
                throw new ArgumentException(LanguageResources.GuidCannotBeEmpty, "userId");

            Content = content;
            UserId = userId;
            ReceivedAt = receivedAt;
        }

        public string Content { get; private set; }

        public Guid UserId { get; private set; }
        
        public DateTimeOffset ReceivedAt { get; private set; }
    }
}