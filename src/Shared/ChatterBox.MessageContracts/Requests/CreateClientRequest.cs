using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Requests
{
    public class CreateClientRequest : IBusRequest<CreateClientRequest, CreateClientResponse>
    {
        public CreateClientRequest(Guid clientId, string userAgent, Guid userId)
        {
            ClientId = clientId;
            UserId = userId;
            UserAgent = userAgent;
        }

        public Guid ClientId { get; protected set; }

        public string UserAgent { get; protected set; }

        public Guid UserId { get; protected set; }
    }
}