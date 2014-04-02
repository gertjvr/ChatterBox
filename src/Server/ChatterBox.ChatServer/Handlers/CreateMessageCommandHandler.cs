using System.Threading.Tasks;
using ChatterBox.MessageContracts.Commands;
using Nimbus.Handlers;

namespace ChatterBox.ChatServer.Handlers
{
    public class CreateMessageCommandHandler : IHandleCommand<CreateMessageCommand>
    {
        public Task Handle(CreateMessageCommand busCommand)
        {
            throw new System.NotImplementedException();
        }
    }
}