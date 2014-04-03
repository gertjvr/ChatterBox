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

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}