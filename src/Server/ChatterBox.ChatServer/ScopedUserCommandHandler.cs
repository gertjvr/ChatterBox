using System;
using System.Threading.Tasks;
using ChatterBox.Core.Persistence;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.Domain.Extensions;
using ChatterBox.MessageContracts.Commands;
using Nimbus.Handlers;

namespace ChatterBox.ChatServer
{
    public abstract class ScopedUserCommandHandler<TBusCommand> : IHandleCommand<TBusCommand> where TBusCommand : IUserCommand
    {
        private readonly Func<IUnitOfWork> _unitOfWork;

        public ScopedUserCommandHandler(Func<IUnitOfWork> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(TBusCommand command)
        {
            using (var context = _unitOfWork())
            {
                try
                {
                    var repository = context.Repository<User>();
                    var user = repository.VerifyUser(command.UserId);

                    await Execute(context, user, command);
                }
                catch
                {
                    context.Abandon();
                    throw;
                }
            }
        }

        public abstract Task Execute(IUnitOfWork context, User callingUser, TBusCommand command);
    }
}