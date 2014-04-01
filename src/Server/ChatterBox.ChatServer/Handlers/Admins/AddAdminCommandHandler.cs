using System.Threading.Tasks;
using ChatterBox.MessageContracts.Admins.Commands;
using Nimbus.Handlers;

namespace ChatterBox.ChatServer.Handlers.Admins
{
    public class AddAdminCommandHandler : IHandleCommand<AddAdminCommand>
    {
        public Task Handle(AddAdminCommand busCommand)
        {
            throw new System.NotImplementedException();
        }
    }
}