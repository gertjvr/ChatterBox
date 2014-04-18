using System;
using ChatterBox.MessageContracts.Dtos;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Users.Requests
{
    public class UserInfoResponse : IBusResponse
    {
        protected UserInfoResponse()
        {
        }

        public UserInfoResponse(UserDto user)
        {
            if (user == null) 
                throw new ArgumentNullException("user");

            User = user;
        }

        public UserDto User { get; private set; }
    }
}