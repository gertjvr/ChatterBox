using System;
using System.Threading.Tasks;
using ChatterBox.Core.Persistence;
using ChatterBox.MessageContracts.Commands;

namespace ChatterBox.ChatServer.Handlers.Owners
{
    public class LockRoomCommandHandler : ScopedCommandHandler<LockRoomCommand>
    {
        public LockRoomCommandHandler(Func<IUnitOfWork> unitOfWork) : base(unitOfWork)
        {
        }

        public override Task Execute(IUnitOfWork context, LockRoomCommand command)
        {
            throw new NotImplementedException();
        }
    }
}