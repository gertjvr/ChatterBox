using ChatterBox.MessageContracts.Dtos;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Requests
{
    public class GetUserInfoResponse : IBusResponse
    {
        protected GetUserInfoResponse()
        {
        }

        public GetUserInfoResponse(UserDto user)
        {
            User = user;
        }

        public UserDto User { get; set; }
    }
}