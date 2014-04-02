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

        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}