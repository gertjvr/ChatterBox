using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Users.Requests
{
    public class RegisterUserRequest : IBusRequest<RegisterUserRequest, RegisterUserResponse>
    {
        protected RegisterUserRequest()
        {   
        }
        
        public RegisterUserRequest(string userName, string emailAddress, string password)
        {
            if (userName == null) 
                throw new ArgumentNullException("userName");
            
            if (emailAddress == null) 
                throw new ArgumentNullException("emailAddress");
            
            if (password == null) 
                throw new ArgumentNullException("password");

            UserName = userName;
            EmailAddress = emailAddress;
            Password = password;
        }

        public string UserName { get; private set; }
        
        public string EmailAddress { get; private set; }

        public string Password { get; private set; }
    }
}