using System;
using System.Threading.Tasks;
using ChatterBox.Core.Persistence;
using ChatterBox.MessageContracts.Commands;

namespace ChatterBox.ChatServer.Handlers
{
    public class AddOwnerCommandHandler : ScopedCommandHandler<AddOwnerCommand>
    {
        public AddOwnerCommandHandler(Func<IUnitOfWork> unitOfWork) : base(unitOfWork)
        {
        }

        public override Task Execute(IUnitOfWork context, AddOwnerCommand command)
        {
            throw new NotImplementedException();
        }
    }
}