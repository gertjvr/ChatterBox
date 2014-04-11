using System;
using System.Threading.Tasks;
using ChatterBox.Core.Persistence;
using ChatterBox.MessageContracts.Commands;

namespace ChatterBox.ChatServer.Handlers
{
    public class ChangeTopicCommandHandler : ScopedCommandHandler<ChangeTopicCommand>
    {
        public ChangeTopicCommandHandler(Func<IUnitOfWork> unitOfWork) : base(unitOfWork)
        {
        }

        public override Task Execute(IUnitOfWork context, ChangeTopicCommand command)
        {
            throw new NotImplementedException();
        }
    }
}