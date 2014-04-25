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
    public class UnallowUserCommandHandler : IHandleCommand<UnallowUserCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Room> _roomRepository;
        private readonly IBus _bus;

        public UnallowUserCommandHandler(
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

        public async Task Handle(UnallowUserCommand command)
        {
            try
            {
                var callingUser = _userRepository.VerifyUser(command.CallingUserId);
                var targetUser = _userRepository.VerifyUser(command.TargetUserId);
                var targetRoom = _roomRepository.VerifyRoom(command.TargetRoomId);

                if (!targetRoom.Owners.Contains(targetUser.Id) && targetUser.IsAdministrator())
                {
                    throw new Exception("You are not an owner of {0}.".FormatWith(targetRoom.Name));
                }

                if (targetUser == callingUser)
                {
                    throw new Exception("Why would you want to unallow yourself?");
                }

                if (targetRoom.PrivateRoom == false)
                {
                    throw new Exception("{0} is not a private room.".FormatWith(targetRoom.Name));
                }

                if (targetRoom.AllowedUsers.Contains(targetUser.Id) == false)
                {
                    throw new Exception("{0} isn't allowed to access {1}.".FormatWith(targetUser.Name, targetRoom.Name));
                }

                if (callingUser.IsAdministrator() == false && targetUser.IsAdministrator())
                {
                    throw new Exception("You cannot unallow an admin. Only admin can unallow admin.");
                }

                if (targetRoom.CreatorId != callingUser.Id && targetRoom.Owners.Contains(targetUser.Id) && callingUser.IsAdministrator() == false)
                {
                    throw new Exception("Owners cannot unallow other owners. Only the room creator can unallow an owner.");
                }

                targetRoom.UnallowUser(targetUser);

                //TODO Make the user leave the room

                _unitOfWork.Complete();

                //TODO Publish UserUnallowedEvent
            }
            catch
            {
                _unitOfWork.Abandon();
                throw;
            }
        }
    }
}