using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Rooms.Requests
{
    public class CreateRoomResponse : IBusResponse
    {
        public Guid RoomId { get; set; }
    }
}