using System;
using System.Threading.Tasks;
using ChatterBox.MessageContracts.Users.Requests;
using Nimbus.Handlers;

namespace ChatterBox.ChatServer.Handlers.Users
{
    public class ConnectClientRequestHandler : IHandleRequest<ConnectClientRequest, ConnectClientResponse>
    {
        public Task<ConnectClientResponse> Handle(ConnectClientRequest request)
        {
            throw new NotImplementedException();
        }
    }
}