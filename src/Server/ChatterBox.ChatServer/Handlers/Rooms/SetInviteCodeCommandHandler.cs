using System;
using System.Threading.Tasks;
using ChatterBox.Core.Infrastructure;
using ChatterBox.Domain.Aggregates.RoomAggregate;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.Domain.Extensions;
using ChatterBox.Domain.Properties;
using ChatterBox.MessageContracts.Rooms.Commands;
using Nimbus;
using Nimbus.Handlers;

namespace ChatterBox.ChatServer.Handlers.Rooms
{
    public class SetInviteCodeCommandHandler : IHandleCommand<SetInviteCodeCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Room> _roomRepository;
        private readonly IBus _bus;

        public SetInviteCodeCommandHandler(
            IUnitOfWork unitOfWork, 
            IRepository<User> userRepository,
            IRepository<Room> roomRepository,
            IBus bus)
        {
            if (unitOfWork == null) 
                throw new ArgumentNullException("unitOfWork");
            
            if (userRepository == null) 
                throw new ArgumentNullException("userRepository");
            
            if (roomRepository == null) 
                throw new ArgumentNullException("roomRepository");
            
            if (bus == null) 
                throw new ArgumentNullException("bus");

            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _roomRepository = roomRepository;
            _bus = bus;
        }

        public async Task Handle(SetInviteCodeCommand command)
        {
            if (command == null) 
                throw new ArgumentNullException("command");

            try
            {
                var callingUser = _userRepository.VerifyUser(command.CallingUserId);
                var targetRoom = _roomRepository.VerifyRoom(command.TargetRoomId);

                targetRoom.EnsureOwnerOrAdmin(callingUser);

                if (!targetRoom.PrivateRoom)
                {
                    throw new InvalidOperationException(LanguageResources.InviteCode_PrivateRoomRequired);
                }

                targetRoom.SetInviteCode(command.InviteCode);
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