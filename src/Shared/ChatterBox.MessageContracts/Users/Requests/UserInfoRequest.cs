using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Users.Requests
{
    public class UserInfoRequest : IBusRequest<UserInfoRequest, UserInfoResponse>
    {
        protected UserInfoRequest()
        {   
        }

        public UserInfoRequest(Guid targetUserId, Guid callingUserId)
        {
            if (targetUserId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "targetUserId");

            if (callingUserId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "callingUserId");

            TargetUserId = targetUserId;
            CallingUserId = callingUserId;
        }

        public Guid TargetUserId { get; private set; }

        public Guid CallingUserId { get; private set; }
    }
}