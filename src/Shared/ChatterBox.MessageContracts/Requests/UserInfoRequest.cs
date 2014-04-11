using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Requests
{
    public class UserInfoRequest : IBusRequest<UserInfoRequest, UserInfoResponse>
    {
        protected UserInfoRequest()
        {   
        }

        public UserInfoRequest(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; protected set; }
    }
}