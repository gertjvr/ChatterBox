using System;
using System.Linq;
using ChatterBox.Core.Infrastructure.Queries;
using ChatterBox.Domain.Aggregates.MessageAggregate;

namespace ChatterBox.Domain.Queries
{
    public class PreviousMessagesQuery : IQuery<Message>
    {
        private readonly Guid _fromId;
        private readonly int _numberOfMessages;

        public PreviousMessagesQuery(Guid fromId, int numberOfMessages)
        {
            _fromId = fromId;
            _numberOfMessages = numberOfMessages;
        }

        public IQueryable<Message> Execute(IQueryable<Message> source)
        {
            return source.OrderByDescending(message => message.CreatedAt)
                .SkipWhile(message => message.Id != _fromId)
                .Take(_numberOfMessages);
        }
    }
}