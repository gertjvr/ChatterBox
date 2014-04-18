using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Users.Requests
{
    public class UserInfoRequest : IBusRequest<UserInfoRequest, UserInfoResponse>
    {
        protected UserInfoRequest()
        {   
        }

        public UserInfoRequest(Guid userId)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "userId");

            UserId = userId;
        }

        public Guid UserId { get; private set; }
    }
}