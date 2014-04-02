using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Requests
{
    public class CreateRoomResponse : IBusResponse
    {
        public Guid RoomId { get; set; }
    }
}