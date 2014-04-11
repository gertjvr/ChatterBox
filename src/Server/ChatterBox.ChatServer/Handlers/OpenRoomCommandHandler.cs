using System;
using System.Threading.Tasks;
using ChatterBox.Core.Persistence;
using ChatterBox.MessageContracts.Commands;

namespace ChatterBox.ChatServer.Handlers
{
    public class OpenRoomCommandHandler : ScopedCommandHandler<OpenRoomCommand>
    {
        public OpenRoomCommandHandler(Func<IUnitOfWork> unitOfWork) : base(unitOfWork)
        {
        }

        public override Task Execute(IUnitOfWork context, OpenRoomCommand command)
        {
            throw new NotImplementedException();
        }
    }
}