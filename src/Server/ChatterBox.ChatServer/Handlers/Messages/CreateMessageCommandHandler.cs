using System.Threading.Tasks;
using ChatterBox.MessageContracts.Messages.Commands;
using Nimbus.Handlers;

namespace ChatterBox.ChatServer.Handlers.Messages
{
    public class CreateMessageCommandHandler : IHandleCommand<CreateMessageCommand>
    {
        public Task Handle(CreateMessageCommand busCommand)
        {
            throw new System.NotImplementedException();
        }
    }
}