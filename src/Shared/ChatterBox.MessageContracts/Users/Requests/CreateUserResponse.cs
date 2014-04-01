using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Users.Requests
{
    public class CreateUserResponse : IBusResponse
    {
        public Guid UserId { get; set; }

        protected CreateUserResponse()
        {
            
        }

        public CreateUserResponse(Guid userId)
        {
            UserId = userId;
        }
    }
}