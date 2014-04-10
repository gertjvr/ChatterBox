using System.Collections.Generic;
using ChatterBox.MessageContracts.Dtos;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Requests
{
    public class GetAllowedRoomsResponse : IBusResponse
    {
        protected GetAllowedRoomsResponse()
        {   
        }

        public GetAllowedRoomsResponse(IEnumerable<RoomDto> rooms)
        {
            Rooms = rooms;
        }

        public IEnumerable<RoomDto> Rooms { get; protected set; }
    }
}