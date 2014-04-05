using ChatterBox.MessageContracts.Dtos;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Requests
{
    public class GetRoomInfoResponse : IBusResponse
    {
        protected GetRoomInfoResponse()
        {
        }

        public GetRoomInfoResponse(RoomDto room)
        {
            Room = room;
        }

        public RoomDto Room { get; set; }
    }
}