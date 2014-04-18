using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Users.Requests
{
    public class CreateClientRequest : IBusRequest<CreateClientRequest, CreateClientResponse>
    {
        protected CreateClientRequest()
        {   
        }
        
        public CreateClientRequest(Guid clientId, string userAgent, Guid userId)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "userId");
            
            if (userAgent == null) 
                throw new ArgumentNullException("userAgent");

            if (clientId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "clientId");

            ClientId = clientId;
            UserId = userId;
            UserAgent = userAgent;
        }

        public Guid ClientId { get; private set; }

        public string UserAgent { get; private set; }

        public Guid UserId { get; private set; }
    }
}