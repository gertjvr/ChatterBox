using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Requests
{
    public class CreateRoomResponse : IBusResponse
    {
        protected CreateRoomResponse()
        {
        }

        public CreateRoomResponse(Guid roomId)
        {
            RoomId = roomId;
        }

        public Guid RoomId { get; set; }
    }
}