using System.Threading.Tasks;
using ChatterBox.MessageContracts.Commands;
using Nimbus.Handlers;

namespace ChatterBox.ChatServer.Handlers
{
    public class RemoveAdminCommandHandler : IHandleCommand<RemoveAdminCommand>
    {
        public Task Handle(RemoveAdminCommand busCommand)
        {
            throw new System.NotImplementedException();
        }
    }
}