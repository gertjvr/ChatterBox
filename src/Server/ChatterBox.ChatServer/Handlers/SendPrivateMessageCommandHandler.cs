using System;
using System.Threading.Tasks;
using ChatterBox.Core.Persistence;
using ChatterBox.MessageContracts.Commands;

namespace ChatterBox.ChatServer.Handlers
{
    public class SendPrivateMessageCommandHandler : ScopedCommandHandler<SendPrivateMessageCommand>
    {
        public SendPrivateMessageCommandHandler(Func<IUnitOfWork> unitOfWork) : base(unitOfWork)
        {
        }

        public override Task Execute(IUnitOfWork context, SendPrivateMessageCommand command)
        {
            throw new NotImplementedException();
        }
    }
}