using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Requests
{
    public class AuthenticateUserResponse : IBusResponse
    {
        protected AuthenticateUserResponse()
        {
        }

        public AuthenticateUserResponse(bool isAutenticated, Guid userId)
        {
            IsAutenticated = isAutenticated;
            UserId = userId;
        }

        public bool IsAutenticated { get; set; }

        public Guid UserId { get; set; }
    }
}