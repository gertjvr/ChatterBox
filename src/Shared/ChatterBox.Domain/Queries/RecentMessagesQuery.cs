using System.Linq;
using ChatterBox.Core.Infrastructure.Queries;
using ChatterBox.Domain.Aggregates.MessageAggregate;

namespace ChatterBox.Domain.Queries
{
    public class RecentMessagesQuery : IQuery<Message>
    {
        private readonly int _numberOfMessages;

        public RecentMessagesQuery(int numberOfMessages)
        {
            _numberOfMessages = numberOfMessages;
        }

        public IQueryable<Message> Execute(IQueryable<Message> source)
        {
            return source.OrderByDescending(message => message.CreatedAt).Take(_numberOfMessages);
        }
    }
}