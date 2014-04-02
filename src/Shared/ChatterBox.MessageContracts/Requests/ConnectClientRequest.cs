using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Requests
{
    public class ConnectClientRequest : IBusRequest<ConnectClientRequest, ConnectClientResponse>
    {
        protected ConnectClientRequest()
        {
        }

        public ConnectClientRequest(Guid userId, string userAgent)
        {
            UserId = userId;
            UserAgent = userAgent;
        }

        public Guid UserId { get; set; }

        public string UserAgent { get; set; }
    }
}