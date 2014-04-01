using System.Threading.Tasks;
using ChatterBox.MessageContracts.Admins.Commands;
using Nimbus.Handlers;

namespace ChatterBox.ChatServer.Handlers.Admins
{
    public class RemoveAdminCommandHandler : IHandleCommand<RemoveAdminCommand>
    {
        public Task Handle(RemoveAdminCommand busCommand)
        {
            throw new System.NotImplementedException();
        }
    }
}