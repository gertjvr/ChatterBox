using System;
using System.Threading.Tasks;
using ChatterBox.Core.Persistence;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.Domain.Extensions;
using ChatterBox.MessageContracts;
using Nimbus;
using Nimbus.Handlers;

namespace ChatterBox.ChatServer
{
    public abstract class ScopedUserCommandHandler<TBusCommand> : IHandleCommand<TBusCommand>
        where TBusCommand : IUserBusCommand
    {
        private readonly Func<IUnitOfWork> _unitOfWork;
        protected readonly IBus _bus;

        public ScopedUserCommandHandler(Func<IUnitOfWork> unitOfWork, IBus bus)
        {
            _unitOfWork = unitOfWork;
            _bus = bus;
        }

        public async Task Handle(TBusCommand command)
        {
            using (IUnitOfWork context = _unitOfWork())
            {
                try
                {
                    IRepository<User> repository = context.Repository<User>();
                    User user = repository.VerifyUser(command.UserId);

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