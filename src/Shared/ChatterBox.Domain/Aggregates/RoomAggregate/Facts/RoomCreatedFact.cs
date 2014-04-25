using System;
using ChatterBox.Core.Infrastructure.Facts;
using ChatterBox.Domain.Properties;

namespace ChatterBox.Domain.Aggregates.RoomAggregate.Facts
{
    public class RoomCreatedFact : FactAbout<Room>
    {
        public RoomCreatedFact(
            Guid aggregateRootId, 
            string name, 
            Guid creatorId,
            string welcome,
            string topic,
            bool privateRoom)
            : base(aggregateRootId)
        {
            if (name == null) 
                throw new ArgumentNullException("name");

            if (creatorId == Guid.Empty)
                throw new ArgumentException(LanguageResources.GuidCannotBeEmpty, "creatorId");
            
            if (welcome == null) 
                throw new ArgumentNullException("welcome");
            
            if (topic == null) 
                throw new ArgumentNullException("topic");

            Name = name;
            CreatorId = creatorId;
            Welcome = welcome;
            Topic = topic;
            PrivateRoom = privateRoom;
        }

        public string Name { get; private set; }

        public Guid CreatorId { get; private set; }
        
        public string Welcome { get; private set; }
        
        public string Topic { get; private set; }

        public bool PrivateRoom { get; private set; }
    }
}