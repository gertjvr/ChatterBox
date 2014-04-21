using System;
using System.Linq;
using System.Threading.Tasks;
using ChatterBox.Core.Infrastructure;
using ChatterBox.Domain.Aggregates.RoomAggregate;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.Domain.Extensions;
using ChatterBox.Domain.Properties;
using ChatterBox.MessageContracts.Owners.Commands;
using ChatterBox.MessageContracts.Owners.Events;
using Nimbus;
using Nimbus.Handlers;
using ThirdDrawer.Extensions.StringExtensionMethods;

namespace ChatterBox.ChatServer.Handlers.Owners
{
    public class AllowUserCommandHandler : IHandleCommand<AllowUserCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Room> _roomRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IBus _bus;

        public AllowUserCommandHandler(
            IUnitOfWork unitOfWork,
            IRepository<Room> roomRepository,
            IRepository<User> userRepository,
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
            _roomRepository = roomRepository;
            _userRepository = userRepository;
            _bus = bus;
        }

        public async Task Handle(AllowUserCommand command)
        {
            try
            {
                var callingUser = _userRepository.VerifyUser(command.CallingUserId);
                var targetRoom = _roomRepository.VerifyRoom(command.TargetRoomId);
                var targetUser = _userRepository.VerifyUser(command.TargetUserId);

                targetRoom.EnsureOwnerOrAdmin(callingUser);

                if (targetRoom.PrivateRoom == false)
                {
                    throw new Exception(LanguageResources.RoomNotPrivate.FormatWith(targetRoom.Name));
                }

                if (targetRoom.AllowedUsers.Contains(targetUser.Id))
                {
                    throw new Exception(LanguageResources.RoomUserAlreadyAllowed.FormatWith(targetUser.Name, targetRoom.Name));
                }

                targetRoom.AllowUser(targetUser);

                _unitOfWork.Complete();

                await _bus.Publish(new UserAllowedEvent(targetUser.Id, targetUser.Name, targetRoom.Id, targetRoom.Name));
            }
            catch
            {
                _unitOfWork.Abandon();
                throw;
            }
        }
    }
}