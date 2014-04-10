using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Commands
{
    public class UpdateActivityCommand : IBusCommand
    {
        protected UpdateActivityCommand()
        {   
        }

        public UpdateActivityCommand(Guid userId, string clientId, string userAgent)
        {
            UserId = userId;
            ClientId = clientId;
            UserAgent = userAgent;
        }

        public Guid UserId { get; protected set; }

        public string ClientId { get; protected set; }
        
        public string UserAgent  { get; protected set; }
    }
}