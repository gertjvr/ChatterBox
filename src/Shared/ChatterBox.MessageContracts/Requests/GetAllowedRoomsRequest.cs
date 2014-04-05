using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Requests
{
    public class GetAllowedRoomsRequest : IBusRequest<GetAllowedRoomsRequest, GetAllowedRoomsResponse>
    {
        public Guid UserId { get; set; }

        protected GetAllowedRoomsRequest()
        {
            
        }

        public GetAllowedRoomsRequest(Guid userId)
        {
            UserId = userId;
        }
    }
}