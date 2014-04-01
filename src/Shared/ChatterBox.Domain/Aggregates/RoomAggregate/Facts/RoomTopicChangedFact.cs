using System;
using ChatterBox.Core.Infrastructure.Facts;

namespace Domain.Aggregates.RoomAggregate.Facts
{
    [Serializable]
    public class RoomTopicChangedFact : FactAbout<Room>
    {
        public string Topic { get; set; }
    }
}