using System;
using System.Threading.Tasks;
using ChatterBox.Core.Infrastructure;
using ChatterBox.Domain.Aggregates.RoomAggregate;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.Domain.Extensions;
using ChatterBox.MessageContracts.Rooms.Commands;
using Nimbus.Handlers;

namespace ChatterBox.ChatServer.Handlers.Rooms
{
    public class LeaveRoomCommandHandler : IHandleCommand<LeaveRoomCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Room> _roomRepository;

        public LeaveRoomCommandHandler(
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

        public async Task Handle(LeaveRoomCommand command)
        {
            try
            {
                var callingUser = _roomRepository.VerifyRoom(command.CallingUserId);
                var targetRoom = _roomRepository.GetById(command.TargetRoomId);
                var targetUser = _userRepository.GetById(command.TargetUserId);

                targetRoom.Leave(targetUser);

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