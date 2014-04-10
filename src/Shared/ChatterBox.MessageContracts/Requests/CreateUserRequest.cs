using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Requests
{
    public class CreateUserRequest : IBusRequest<CreateUserRequest, CreateUserResponse>
    {
        protected CreateUserRequest()
        {   
        }

        public CreateUserRequest(string userName, string email, string password)
        {
            UserName = userName;
            Email = email;
            Password = password;
        }

        public string UserName { get; protected set; }
        
        public string Email { get; protected set; }

        public string Password { get; protected set; }
    }
}