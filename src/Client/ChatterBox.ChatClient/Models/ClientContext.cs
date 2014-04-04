using System;

namespace ChatterBox.ChatClient.Models
{
    public class ClientContext
    {
        public ClientContext()
        {
            ClientId = Guid.NewGuid();
        }
        
        public Guid ClientId { get; private set; }

        public void SetClientId(Guid clientId)
        {
            ClientId = clientId;
        }
    }
}
