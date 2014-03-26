using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Events
{
    public class ClientConnectedEvent : IBusEvent
    {
        public Guid ClientId { get; set; }

        public DateTimeOffset ConnectedAt { get; set; }

        protected ClientConnectedEvent()
        {

        }

        public ClientConnectedEvent(Guid clientId, DateTimeOffset connectedAt)
        {
            ClientId = clientId;
            ConnectedAt = connectedAt;
        }
    }
}