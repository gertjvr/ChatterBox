using System;
using ChatterBox.Core.Infrastructure.Facts;

namespace ChatterBox.Domain.Aggregates.RoomAggregate.Facts
{
    public class RoomWelcomeMessageUpdatedFact : FactAbout<Room>
    {
        public RoomWelcomeMessageUpdatedFact(
            Guid aggregateRootId, 
            string newWelcomeMessage) 
            : base(aggregateRootId)
        {
            if (newWelcomeMessage == null) 
                throw new ArgumentNullException("newWelcomeMessage");

            NewWelcomeMessage = newWelcomeMessage;
        }

        public string NewWelcomeMessage { get; private set; }
    }
}