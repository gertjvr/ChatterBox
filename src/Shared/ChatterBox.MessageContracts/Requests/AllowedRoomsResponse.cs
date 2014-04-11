using System.Collections.Generic;
using ChatterBox.MessageContracts.Dtos;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Requests
{
    public class AllowedRoomsResponse : IBusResponse
    {
        protected AllowedRoomsResponse()
        {   
        }

        public AllowedRoomsResponse(IEnumerable<RoomDto> rooms)
        {
            Rooms = rooms;
        }

        public IEnumerable<RoomDto> Rooms { get; protected set; }
    }
}