using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Requests
{
    public class AllowedRoomsRequest : IBusRequest<AllowedRoomsRequest, AllowedRoomsResponse>
    {
        protected AllowedRoomsRequest()
        {   
        }

        public AllowedRoomsRequest(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; protected set; }
    }
}