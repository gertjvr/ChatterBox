using System;
using System.Threading.Tasks;
using ChatterBox.Core.Infrastructure;
using ChatterBox.MessageContracts.Users.Commands;
using Nimbus.Handlers;

namespace ChatterBox.ChatServer.Handlers.Users
{
    public class DisconnectClientCommandHandler : IHandleCommand<DisconnectClientCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DisconnectClientCommandHandler(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null) 
                throw new ArgumentNullException("unitOfWork");

            _unitOfWork = unitOfWork;
        }

        public Task Handle(DisconnectClientCommand command)
        {
            throw new NotImplementedException();
        }
    }
}