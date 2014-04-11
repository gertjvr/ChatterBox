using System;
using System.Threading.Tasks;
using ChatterBox.Core.Persistence;
using ChatterBox.MessageContracts.Commands;

namespace ChatterBox.ChatServer.Handlers
{
    public class AllowUserCommandHandler : ScopedCommandHandler<AllowUserCommand>
    {
        public AllowUserCommandHandler(Func<IUnitOfWork> unitOfWork) : base(unitOfWork)
        {
        }

        public override Task Execute(IUnitOfWork context, AllowUserCommand command)
        {
            throw new NotImplementedException();
        }
    }
}