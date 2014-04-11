using System;
using System.Threading.Tasks;
using ChatterBox.Core.Persistence;
using ChatterBox.MessageContracts.Commands;

namespace ChatterBox.ChatServer.Handlers
{
    public class BanUserCommandHandler : ScopedCommandHandler<BanUserCommand>
    {
        public BanUserCommandHandler(Func<IUnitOfWork> unitOfWork) : base(unitOfWork)
        {
        }

        public override Task Execute(IUnitOfWork context, BanUserCommand command)
        {
            throw new NotImplementedException();
        }
    }
}