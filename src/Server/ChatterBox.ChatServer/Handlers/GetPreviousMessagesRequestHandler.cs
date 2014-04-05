using System;
using System.Threading.Tasks;
using ChatterBox.MessageContracts.Requests;
using Nimbus.Handlers;

namespace ChatterBox.ChatServer.Handlers
{
    public class GetPreviousMessagesRequestHandler : IHandleRequest<GetPreviousMessagesRequest, GetPreviousMessagesResponse>
    {
        public Task<GetPreviousMessagesResponse> Handle(GetPreviousMessagesRequest request)
        {
            throw new NotImplementedException();
        }
    }
}