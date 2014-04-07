using ChatterBox.MessageContracts.Dtos;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Requests
{
    public class RoomInfoResponse : IBusResponse
    {
        protected RoomInfoResponse()
        {
        }

        public RoomInfoResponse(RoomDto room)
        {
            Room = room;
        }

        public RoomDto Room { get; set; }
    }
}