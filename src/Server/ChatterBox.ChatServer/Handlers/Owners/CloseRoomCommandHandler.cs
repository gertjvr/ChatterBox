using System;
using System.Threading.Tasks;
using ChatterBox.Core.Infrastructure;
using ChatterBox.Domain.Aggregates.RoomAggregate;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.Domain.Extensions;
using ChatterBox.MessageContracts.Owners.Commands;
using ChatterBox.MessageContracts.Owners.Events;
using Nimbus;
using Nimbus.Handlers;
using ThirdDrawer.Extensions.StringExtensionMethods;

namespace ChatterBox.ChatServer.Handlers.Owners
{
    public class CloseRoomCommandHandler : IHandleCommand<CloseRoomCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Room> _roomRepository;
        private readonly IBus _bus;

        public CloseRoomCommandHandler(
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

        public async Task Handle(CloseRoomCommand command)
        {
            try
            {
                var callingUser = _userRepository.VerifyUser(command.CallingUserId);
                var targetRoom = _roomRepository.VerifyRoom(command.TargetRoomId);

                targetRoom.EnsureOwnerOrAdmin(callingUser);

                if (targetRoom.Closed)
                {
                    throw new Exception("{0} is already closed.".FormatWith(targetRoom.Name));
                }

                targetRoom.Close();

                _unitOfWork.Complete();

                await _bus.Publish(new RoomClosedEvent(targetRoom.Id, targetRoom.Name));
            }
            catch
            {
                _unitOfWork.Abandon();
                throw;
            }
        }
    }
}