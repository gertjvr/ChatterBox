using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Requests
{
    public class GetUserInfoRequest : IBusRequest<GetUserInfoRequest, GetUserInfoResponse>
    {
        protected GetUserInfoRequest()
        {
        }

        public GetUserInfoRequest(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; set; }
    }
}