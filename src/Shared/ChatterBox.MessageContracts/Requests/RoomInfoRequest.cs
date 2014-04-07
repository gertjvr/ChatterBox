using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Requests
{
    public class RoomInfoRequest : IBusRequest<RoomInfoRequest, RoomInfoResponse>
    {
        protected RoomInfoRequest()
        {
        }

        public RoomInfoRequest(Guid roomId)
        {
            RoomId = roomId;
        }

        public Guid RoomId { get; set; }
    }
}