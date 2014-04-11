using System;
using System.Threading.Tasks;
using ChatterBox.Core.Persistence;
using ChatterBox.MessageContracts.Commands;

namespace ChatterBox.ChatServer.Handlers
{
    public class UpdateActivityCommandHandler : ScopedCommandHandler<UpdateActivityCommand>
    {
        public UpdateActivityCommandHandler(Func<IUnitOfWork> unitOfWork) : base(unitOfWork)
        {
        }

        public override Task Execute(IUnitOfWork context, UpdateActivityCommand command)
        {
            throw new NotImplementedException();
        }
    }
}