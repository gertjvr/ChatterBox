using System;
using System.Threading.Tasks;
using ChatterBox.Core.Infrastructure;
using ChatterBox.MessageContracts.Owners.Commands;
using Nimbus.Handlers;

namespace ChatterBox.ChatServer.Handlers.Owners
{
    public class LockRoomCommandHandler : IHandleCommand<LockRoomCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public LockRoomCommandHandler(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null) 
                throw new ArgumentNullException("unitOfWork");

            _unitOfWork = unitOfWork;
        }

        public Task Handle(LockRoomCommand command)
        {
            throw new NotImplementedException();
        }
    }
}