using System;
using System.Threading.Tasks;
using ChatterBox.Core.Persistence;
using ChatterBox.MessageContracts.Commands;

namespace ChatterBox.ChatServer.Handlers
{
    public class CloseRoomCommandHandler : ScopedCommandHandler<CloseRoomCommand>
    {
        public CloseRoomCommandHandler(Func<IUnitOfWork> unitOfWork) : base(unitOfWork)
        {
        }

        public override Task Execute(IUnitOfWork context, CloseRoomCommand command)
        {
            throw new NotImplementedException();
        }
    }
}