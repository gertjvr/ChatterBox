using System;
using System.Threading.Tasks;
using ChatterBox.Core.Persistence;
using ChatterBox.MessageContracts.Users.Commands;

namespace ChatterBox.ChatServer.Handlers.Users
{
    public class DisconnectClientCommandHandler : ScopedCommandHandler<DisconnectClientCommand>
    {
        public DisconnectClientCommandHandler(Func<IUnitOfWork> unitOfWork) : base(unitOfWork)
        {
        }

        public override Task Execute(IUnitOfWork context, DisconnectClientCommand command)
        {
            throw new NotImplementedException();
        }
    }
}