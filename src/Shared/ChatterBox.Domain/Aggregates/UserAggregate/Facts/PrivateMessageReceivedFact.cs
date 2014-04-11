using System;
using ChatterBox.Core.Infrastructure.Facts;

namespace ChatterBox.Domain.Aggregates.UserAggregate.Facts
{
    public class PrivateMessageReceivedFact : FactAbout<User>
    {
        public PrivateMessageReceivedFact(string content, Guid userId, DateTimeOffset receivedAt)
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