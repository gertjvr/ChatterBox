using System;
using ChatterBox.Core.Infrastructure.Facts;
using ChatterBox.Domain.Properties;

namespace ChatterBox.Domain.Aggregates.UserAggregate.Facts
{
    public class UserPrivateMessageReceivedFact : FactAbout<User>
    {
        public UserPrivateMessageReceivedFact(Guid aggregateRootId, string content, Guid userId, DateTimeOffset receivedAt)
            : base(aggregateRootId)
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