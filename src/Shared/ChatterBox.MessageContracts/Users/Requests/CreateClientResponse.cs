using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Users.Requests
{
    public class CreateClientResponse : IBusResponse
    {
        protected CreateClientResponse()
        {   
        }
        
        public CreateClientResponse(Guid clientId)
        {
            if (clientId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "clientId");

            ClientId = clientId;
        }

        public Guid ClientId { get; private set; }
    }
}