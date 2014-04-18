using System;
using ChatterBox.MessageContracts.Dtos;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Rooms.Requests
{
    public class RoomInfoResponse : IBusResponse
    {
        protected RoomInfoResponse()
        {   
        }
        
        public RoomInfoResponse(RoomDto room)
        {
            if (room == null) 
                throw new ArgumentNullException("room");

            Room = room;
        }

        public RoomDto Room { get; private set; }
    }
}