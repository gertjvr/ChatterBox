using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Events
{
    public class ClientDisconnectedEvent : IBusEvent
    {
        public Guid ClientId { get; set; }

        public DateTimeOffset ConnectedAt { get; set; }

        protected ClientDisconnectedEvent()
        {

        }

        public ClientDisconnectedEvent(Guid clientId, DateTimeOffset connectedAt)
        {
            ClientId = clientId;
            ConnectedAt = connectedAt;
        }
    }
}