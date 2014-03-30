using ChatterBox.Core.Infrastructure.Facts;

namespace Domain.Aggregates.ConversationAggregate.Facts
{
    public class TopicChangedFact : FactAbout<Conversation>
    {
        public string Topic { get; set; }
    }
}