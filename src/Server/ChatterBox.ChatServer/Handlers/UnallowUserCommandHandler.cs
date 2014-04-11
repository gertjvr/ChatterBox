using System;
using System.Threading.Tasks;
using ChatterBox.Core.Persistence;
using ChatterBox.MessageContracts.Commands;

namespace ChatterBox.ChatServer.Handlers
{
    public class UnallowUserCommandHandler : ScopedCommandHandler<UnallowUserCommand>
    {
        public UnallowUserCommandHandler(Func<IUnitOfWork> unitOfWork) : base(unitOfWork)
        {
        }

        public override Task Execute(IUnitOfWork context, UnallowUserCommand command)
        {
            throw new NotImplementedException();
        }
    }
}