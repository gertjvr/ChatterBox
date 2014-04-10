using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Requests
{
    public class CreateUserResponse : IBusResponse
    {
        protected CreateUserResponse()
        {   
        }

        public CreateUserResponse(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; protected set; }
    }
}