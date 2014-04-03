using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Requests
{
    public class ConnectClientResponse : IBusResponse
    {
        protected ConnectClientResponse()
        {
            
        }

        public ConnectClientResponse(Guid clientId)
        {
            ClientId = clientId;
        }

        public Guid ClientId { get; set; }
    }
}