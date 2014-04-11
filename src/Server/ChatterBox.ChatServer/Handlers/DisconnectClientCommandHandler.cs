using System;
using System.Threading.Tasks;
using ChatterBox.Core.Persistence;
using ChatterBox.MessageContracts.Commands;

namespace ChatterBox.ChatServer.Handlers
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