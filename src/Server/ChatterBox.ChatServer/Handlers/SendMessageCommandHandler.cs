using System.Threading.Tasks;
using ChatterBox.MessageContracts.Commands;
using Nimbus.Handlers;

namespace ChatterBox.ChatServer.Handlers
{
    public class SendMessageCommandHandler : IHandleCommand<SendMessageCommand>
    {
        public Task Handle(SendMessageCommand busCommand)
        {
            throw new System.NotImplementedException();
        }
    }
    
    public class SendPrivateMessageCommandHandler : IHandleCommand<SendPrivateMessageCommand>
    {
        public Task Handle(SendPrivateMessageCommand busCommand)
        {
            throw new System.NotImplementedException();
        }
    }
}