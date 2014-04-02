using System;
using System.Threading.Tasks;
using ChatterBox.MessageContracts.Requests;
using Nimbus.Handlers;

namespace ChatterBox.ChatServer.Handlers
{
    public class ConnectClientRequestHandler : IHandleRequest<ConnectClientRequest, ConnectClientResponse>
    {
        public Task<ConnectClientResponse> Handle(ConnectClientRequest request)
        {
            throw new NotImplementedException();
        }
    }
}