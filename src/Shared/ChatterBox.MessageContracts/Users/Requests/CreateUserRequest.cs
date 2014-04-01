using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Users.Requests
{
    public class CreateUserRequest : IBusRequest<CreateUserRequest, CreateUserResponse>
    {
        protected CreateUserRequest()
        {
        }

        public CreateUserRequest(string userName, string email, string salt, string hashedPassword)
        {
            UserName = userName;
            Email = email;
            Salt = salt;
            HashedPassword = hashedPassword;
        }

        public string UserName { get; set; }
        public string Email { get; set; }
        public string Salt { get; set; }
        public string HashedPassword { get; set; }
    }
}