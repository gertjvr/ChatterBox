using System;
using System.Threading.Tasks;
using ChatterBox.Core.Persistence;
using ChatterBox.MessageContracts.Commands;

namespace ChatterBox.ChatServer.Handlers
{
    public class RemoveOwnerCommandHandler : ScopedCommandHandler<RemoveOwnerCommand>
    {
        public RemoveOwnerCommandHandler(Func<IUnitOfWork> unitOfWork) : base(unitOfWork)
        {
        }

        public override Task Execute(IUnitOfWork context, RemoveOwnerCommand command)
        {
            throw new NotImplementedException();
        }
    }
}