using System;
using System.Collections.Generic;
using ChatterBox.MessageContracts.Dtos;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Users.Requests
{
    public class AllowedRoomsResponse : IBusResponse
    {
        protected AllowedRoomsResponse()
        {   
        }
        
        public AllowedRoomsResponse(IEnumerable<RoomDto> rooms)
        {
            if (rooms == null) 
                throw new ArgumentNullException("rooms");

            Rooms = rooms;
        }

        public IEnumerable<RoomDto> Rooms { get; private set; }
    }
}