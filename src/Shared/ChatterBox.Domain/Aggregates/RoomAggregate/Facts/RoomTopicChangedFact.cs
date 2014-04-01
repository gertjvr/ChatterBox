using System;
using ChatterBox.Core.Infrastructure.Facts;

namespace ChatterBox.Domain.Aggregates.RoomAggregate.Facts
{
    [Serializable]
    public class RoomTopicChangedFact : FactAbout<Room>
    {
        public string Topic { get; set; }
    }
}