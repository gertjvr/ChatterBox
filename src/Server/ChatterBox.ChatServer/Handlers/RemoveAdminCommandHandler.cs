using System;
using System.Threading.Tasks;
using ChatterBox.Core.Persistence;
using ChatterBox.MessageContracts.Commands;

namespace ChatterBox.ChatServer.Handlers
{
    public class RemoveAdminCommandHandler : ScopedCommandHandler<RemoveAdminCommand>
    {
        public RemoveAdminCommandHandler(Func<IUnitOfWork> unitOfWork) : base(unitOfWork)
        {
        }

        public override Task Execute(IUnitOfWork context, RemoveAdminCommand command)
        {
            throw new NotImplementedException();
        }
    }
}