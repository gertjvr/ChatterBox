using System;
using System.Threading.Tasks;
using ChatterBox.Core.Persistence;
using ChatterBox.MessageContracts.Commands;

namespace ChatterBox.ChatServer.Handlers
{
    public class JoinRoomCommandHandler : ScopedCommandHandler<JoinRoomCommand>
    {
        public JoinRoomCommandHandler(Func<IUnitOfWork> unitOfWork) : base(unitOfWork)
        {
        }

        public override Task Execute(IUnitOfWork context, JoinRoomCommand command)
        {
            throw new NotImplementedException();
        }
    };
}