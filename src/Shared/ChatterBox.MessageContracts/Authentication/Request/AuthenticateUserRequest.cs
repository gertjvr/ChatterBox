using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Authentication.Request
{
    public class AuthenticateUserRequest : IBusRequest<AuthenticateUserRequest, AuthenticateUserResponse>
    {
        protected AuthenticateUserRequest()
        {   
        }

        public AuthenticateUserRequest(string userNameOrEmail, string password)
        {
            if (userNameOrEmail == null)
                throw new ArgumentNullException("userNameOrEmail");

            if (password == null) 
                throw new ArgumentNullException("password");

            UserNameOrEmail = userNameOrEmail;
            Password = password;
        }

        public string UserNameOrEmail { get; private set; }
        
        public string Password { get; private set; }
    }
}