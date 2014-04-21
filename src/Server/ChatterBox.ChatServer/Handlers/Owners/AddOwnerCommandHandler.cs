using System;
using System.Linq;
using System.Threading.Tasks;
using ChatterBox.Core.Infrastructure;
using ChatterBox.Domain.Aggregates.RoomAggregate;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.Domain.Extensions;
using ChatterBox.MessageContracts.Owners.Commands;
using ChatterBox.MessageContracts.Owners.Events;
using Nimbus;
using Nimbus.Handlers;

namespace ChatterBox.ChatServer.Handlers.Owners
{
    public class AddOwnerCommandHandler : IHandleCommand<AddOwnerCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Room> _roomRepository;
        private readonly IBus _bus;

        public AddOwnerCommandHandler(
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

        public async Task Handle(AddOwnerCommand command)
        {
            try
            {
                var callingUser = _userRepository.VerifyUser(command.CallingUserId);
                var targetUser = _userRepository.VerifyUser(command.TargetUserId);
                var targetRoom = _roomRepository.VerifyRoom(command.TargetRoomId);

                targetRoom.EnsureOwnerOrAdmin(callingUser);

                targetRoom.AddOwner(targetUser);

                if (targetRoom.PrivateRoom)
                {
                    if (targetRoom.AllowedUsers.Contains(targetUser.Id) == false)
                    {
                        targetRoom.AllowUser(targetUser);
                    }
                }

                _unitOfWork.Complete();

                await _bus.Publish(new OwnerAddedEvent(targetUser.Id, targetUser.Name, targetRoom.Id, targetRoom.Name));
            }
            catch
            {
                _unitOfWork.Abandon();
                throw;
            }
        }
    }
}
