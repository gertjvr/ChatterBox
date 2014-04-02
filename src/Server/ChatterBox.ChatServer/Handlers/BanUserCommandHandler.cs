using System.Threading.Tasks;
using ChatterBox.MessageContracts.Commands;
using Nimbus.Handlers;

namespace ChatterBox.ChatServer.Handlers
{
    public class BanUserCommandHandler : IHandleCommand<BanUserCommand>
    {
        public Task Handle(BanUserCommand busCommand)
        {
            throw new System.NotImplementedException();
        }
    }
}