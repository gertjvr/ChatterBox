using System;
using System.Threading.Tasks;
using ChatterBox.Core.Persistence;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.MessageContracts.Users.Commands;
using Nimbus.Handlers;

namespace ChatterBox.ChatServer.Handlers.Users
{
    public class ChangeUserNameCommandHandler : IHandleCommand<ChangeUserNameCommand>
    {
        private readonly Func<IUnitOfWork> _unitOfWorkFactory;

        public ChangeUserNameCommandHandler(Func<IUnitOfWork> unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public Task Handle(ChangeUserNameCommand message)
        {
            using (var unitOfWork = _unitOfWorkFactory())
            {
                var repository = unitOfWork.Repository<User>();

                EnsureUserNameIsAvailable(message.NewUserName);

                var user = repository.GetById(message.UserId);
                user.ChangeUserName(message.NewUserName);

                unitOfWork.Complete();

                return Task.FromResult(0);
            }

        }

        private void EnsureUserNameIsAvailable(string newUserName)
        {   
        }
    }
}