﻿using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Rooms.Requests
{
    public class CreateRoomResponse : IBusResponse
    {
        protected CreateRoomResponse()
        {   
        }

        public CreateRoomResponse(Guid roomId)
        {
            if (roomId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "roomId");
            
            RoomId = roomId;
        }

        public Guid RoomId { get; private set; }
    }
}