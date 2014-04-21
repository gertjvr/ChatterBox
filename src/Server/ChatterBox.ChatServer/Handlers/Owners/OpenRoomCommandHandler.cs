using System;
using System.Threading.Tasks;
using ChatterBox.Core.Infrastructure;
using ChatterBox.Domain.Aggregates.RoomAggregate;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.Domain.Extensions;
using ChatterBox.MessageContracts.Owners.Commands;
using Nimbus.Handlers;

namespace ChatterBox.ChatServer.Handlers.Owners
{
    public class OpenRoomCommandHandler : IHandleCommand<OpenRoomCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Room> _roomRepository;

        public OpenRoomCommandHandler(
            IUnitOfWork unitOfWork,
            IRepository<User> userRepository,
            IRepository<Room> roomRepository)
        {
            if (unitOfWork == null) 
                throw new ArgumentNullException("unitOfWork");
            
            if (userRepository == null) 
                throw new ArgumentNullException("userRepository");
            
            if (roomRepository == null) 
                throw new ArgumentNullException("roomRepository");

            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _roomRepository = roomRepository;
        }

        public async Task Handle(OpenRoomCommand command)
        {
            try
            {
                var callingUser = _userRepository.VerifyUser(command.CallingUserId);
                var targetRoom = _roomRepository.VerifyRoom(command.TargetRoomId);

                targetRoom.Open();

                _unitOfWork.Complete();
            }
            catch
            {
                _unitOfWork.Abandon();
                throw;
            }
        }
    }
}