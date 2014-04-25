using System;
using System.Linq;
using System.Threading.Tasks;
using ChatterBox.Core.Infrastructure;
using ChatterBox.Domain.Aggregates.RoomAggregate;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.Domain.Extensions;
using ChatterBox.MessageContracts.Owners.Commands;
using Nimbus;
using Nimbus.Handlers;
using ThirdDrawer.Extensions.StringExtensionMethods;

namespace ChatterBox.ChatServer.Handlers.Owners
{
    public class RemoveOwnerCommandHandler : IHandleCommand<RemoveOwnerCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Room> _roomRepository;
        private readonly IBus _bus;

        public RemoveOwnerCommandHandler(
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

        public async Task Handle(RemoveOwnerCommand command)
        {
            try
            {
                var callingUser = _userRepository.VerifyUser(command.CallingUserId);
                var targetRoom = _roomRepository.VerifyRoom(command.TargetRoomId);
                var targetUser = _userRepository.VerifyUser(command.TargetUserId);

                EnsureCreatorOrAdmin(callingUser, targetRoom);

                EnsureOwnerOrAdmin(callingUser, targetRoom);

                if (targetRoom.Owners.Contains(targetUser.Id) == false)
                {
                    throw new Exception("{0} is not an owner of {1}.".FormatWith(targetUser.Name, targetRoom.Name));
                }

                targetRoom.RemoveOwner(targetUser);

                _unitOfWork.Complete();

                //TODO Publish OwnerRemovedEvent
            }
            catch
            {
                _unitOfWork.Abandon();
                throw;
            }
        }

        private void EnsureCreatorOrAdmin(User user, Room room)
        {
            if (user.Id != room.CreatorId && user.IsAdministrator() == false)
            {
                throw new Exception("You are not the creator of {0}.".FormatWith(room.Name));
            }
        }

        private void EnsureOwnerOrAdmin(User user, Room room)
        {
            if (!room.Owners.Contains(user.Id) && user.IsAdministrator())
            {
                throw new Exception("You are not an owner of {0}.".FormatWith(room.Name));
            }
        }
    }
}