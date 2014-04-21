using System;
using System.Threading.Tasks;
using ChatterBox.Core.Infrastructure;
using ChatterBox.MessageContracts.Owners.Commands;
using Nimbus.Handlers;

namespace ChatterBox.ChatServer.Handlers.Owners
{
    public class KickUserCommandHandler : IHandleCommand<KickUserCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public KickUserCommandHandler(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null) 
                throw new ArgumentNullException("unitOfWork");

            _unitOfWork = unitOfWork;
        }

        public Task Handle(KickUserCommand command)
        {
            throw new NotImplementedException();
        }
    }
}