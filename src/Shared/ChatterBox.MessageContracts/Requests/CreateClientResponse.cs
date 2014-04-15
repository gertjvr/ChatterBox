using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Requests
{
    public class CreateClientResponse : IBusResponse
    {
        protected CreateClientResponse()
        {   
        }

        public CreateClientResponse(Guid clientId)
        {
            ClientId = clientId;
        }

        public Guid ClientId { get; protected set; }
    }
}