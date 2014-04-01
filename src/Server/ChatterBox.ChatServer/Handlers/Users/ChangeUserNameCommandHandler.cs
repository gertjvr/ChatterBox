using System.Threading.Tasks;
using Autofac;
using ChatterBox.Core.Persistence;
using ChatterBox.MessageContracts.Users.Commands;
using Domain.Aggregates.UserAggregate;
using Nimbus.Handlers;

namespace ChatterBox.ChatServer.Handlers.Users
{
    public class ChangeUserNameCommandHandler : IHandleCommand<ChangeUserNameCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<User> _useRepository;

        public ChangeUserNameCommandHandler(IUnitOfWork unitOfWork, IRepository<User> useRepository)
        {
            _unitOfWork = unitOfWork;
            _useRepository = useRepository;
        }

        public Task Handle(ChangeUserNameCommand message)
        {
            EnsureUserNameIsAvailable(message.NewUserName);

            var user = _useRepository.GetById(message.UserId);
            user.ChangeUserName(message.NewUserName);

            _unitOfWork.Complete();

            return Task.FromResult(0);
        }

        private void EnsureUserNameIsAvailable(string newUserName)
        {   
        }
    }
}