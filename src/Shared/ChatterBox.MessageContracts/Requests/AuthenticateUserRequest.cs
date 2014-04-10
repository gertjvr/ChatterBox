using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Requests
{
    public class AuthenticateUserRequest : IBusRequest<AuthenticateUserRequest, AuthenticateUserResponse>
    {
        protected AuthenticateUserRequest()
        {   
        }

        public AuthenticateUserRequest(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        public string UserName { get; protected set; }

        public string Password { get; protected set; }
    }
}