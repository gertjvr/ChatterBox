using System;
using System.Threading.Tasks;
using ChatterBox.Core.Persistence;
using ChatterBox.MessageContracts.Commands;

namespace ChatterBox.ChatServer.Handlers
{
    public class SetInviteCodeCommandHandler : ScopedCommandHandler<SetInviteCodeCommand>
    {
        public SetInviteCodeCommandHandler(Func<IUnitOfWork> unitOfWork) : base(unitOfWork)
        {
        }

        public override Task Execute(IUnitOfWork context, SetInviteCodeCommand command)
        {
            throw new NotImplementedException();
        }
    }
}