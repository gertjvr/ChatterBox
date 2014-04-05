using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Requests
{
    public class GetRoomInfoRequest : IBusRequest<GetRoomInfoRequest, GetRoomInfoResponse>
    {
        protected GetRoomInfoRequest()
        {
        }

        public GetRoomInfoRequest(Guid roomId)
        {
            RoomId = roomId;
        }

        public Guid RoomId { get; set; }
    }
}