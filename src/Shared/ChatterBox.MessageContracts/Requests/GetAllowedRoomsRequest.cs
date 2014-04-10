using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Requests
{
    public class GetAllowedRoomsRequest : IBusRequest<GetAllowedRoomsRequest, GetAllowedRoomsResponse>
    {
        protected GetAllowedRoomsRequest()
        {   
        }

        public GetAllowedRoomsRequest(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; protected set; }
    }
}