using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Users.Requests
{
    public class CreateUserRequest : IBusRequest<CreateUserRequest, CreateUserResponse>
    {
        protected CreateUserRequest()
        {   
        }
        
        public CreateUserRequest(string userName, string email, string password)
        {
            if (userName == null) 
                throw new ArgumentNullException("userName");
            
            if (email == null) 
                throw new ArgumentNullException("email");
            
            if (password == null) 
                throw new ArgumentNullException("password");

            UserName = userName;
            Email = email;
            Password = password;
        }

        public string UserName { get; private set; }
        
        public string Email { get; private set; }

        public string Password { get; private set; }
    }
}