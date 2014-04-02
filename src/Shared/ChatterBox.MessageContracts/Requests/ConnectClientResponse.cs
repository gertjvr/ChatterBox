using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Requests
{
    public class ConnectClientResponse : IBusResponse
    {
        public Guid ClientId { get; set; }
    }
}