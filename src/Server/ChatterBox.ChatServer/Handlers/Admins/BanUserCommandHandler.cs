using System.Threading.Tasks;
using ChatterBox.MessageContracts.Admins.Commands;
using Nimbus.Handlers;

namespace ChatterBox.ChatServer.Handlers.Admins
{
    public class BanUserCommandHandler : IHandleCommand<BanUserCommand>
    {
        public Task Handle(BanUserCommand busCommand)
        {
            throw new System.NotImplementedException();
        }
    }
}