using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Authentication.Request
{
    public class AuthenticateUserRequest : IBusRequest<AuthenticateUserRequest, AuthenticateUserResponse>
    {
        protected AuthenticateUserRequest()
        {   
        }

        public AuthenticateUserRequest(string userName, string password)
        {
            if (userName == null) 
                throw new ArgumentNullException("userName");
            
            if (password == null) 
                throw new ArgumentNullException("password");

            UserName = userName;
            Password = password;
        }

        public string UserName { get; private set; }

        public string Password { get; private set; }
    }
}