using System;
using System.Threading.Tasks;
using ChatterBox.Core.Persistence;
using ChatterBox.MessageContracts.Commands;

namespace ChatterBox.ChatServer.Handlers
{
    public class KickUserCommandHandler : ScopedCommandHandler<KickUserCommand>
    {
        public KickUserCommandHandler(Func<IUnitOfWork> unitOfWork) : base(unitOfWork)
        {
        }

        public override Task Execute(IUnitOfWork context, KickUserCommand command)
        {
            throw new NotImplementedException();
        }
    }
}