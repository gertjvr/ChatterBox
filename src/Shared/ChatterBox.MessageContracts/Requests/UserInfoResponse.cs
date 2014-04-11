using ChatterBox.MessageContracts.Dtos;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Requests
{
    public class UserInfoResponse : IBusResponse
    {
        protected UserInfoResponse()
        {   
        }

        public UserInfoResponse(UserDto user)
        {
            User = user;
        }

        public UserDto User { get; protected set; }
    }
}