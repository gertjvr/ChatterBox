using System;
using System.Threading.Tasks;
using ChatterBox.ChatServer.ConfigurationSettings;
using ChatterBox.Core.Infrastructure;
using ChatterBox.Domain.Aggregates.MessageAggregate;
using ChatterBox.Domain.Aggregates.RoomAggregate;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.Domain.Extensions;
using ChatterBox.Domain.Properties;
using ChatterBox.MessageContracts.Messages.Commands;
using Nimbus;
using Nimbus.Handlers;
using ThirdDrawer.Extensions.CollectionExtensionMethods;

namespace ChatterBox.ChatServer.Handlers.Messages
{
    public class SendMessageCommandHandler : IHandleCommand<SendMessageCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBus _bus;
        private readonly IRepository<Room> _roomRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Message> _messageRepository;
        private readonly IClock _clock;
        private readonly MaxMessageLengthSetting _maxMessageLengthSetting;

        public SendMessageCommandHandler(
            IUnitOfWork unitOfWork,
            IRepository<Room> roomRepository,
            IRepository<User> userRepository,
            IRepository<Message> messageRepository,
            IClock clock,
            IBus bus,
            MaxMessageLengthSetting maxMessageLengthSetting)
        {
            if (unitOfWork == null) 
                throw new ArgumentNullException("unitOfWork");
            
            if (roomRepository == null) 
                throw new ArgumentNullException("roomRepository");
            
            if (userRepository == null) 
                throw new ArgumentNullException("userRepository");
            
            if (messageRepository == null) 
                throw new ArgumentNullException("messageRepository");
            
            if (clock == null) 
                throw new ArgumentNullException("clock");

            if (maxMessageLengthSetting == null) 
                throw new ArgumentNullException("maxMessageLengthSetting");
            
            if (bus == null) 
                throw new ArgumentNullException("bus");

            _unitOfWork = unitOfWork;
            _roomRepository = roomRepository;
            _userRepository = userRepository;
            _messageRepository = messageRepository;
            _clock = clock;
            _bus = bus;
            _maxMessageLengthSetting = maxMessageLengthSetting;
        }

        public async Task Handle(SendMessageCommand command)
        {
            if (command == null) 
                throw new ArgumentNullException("command");

            try
            {
                var callingUser = _userRepository.GetById(command.CallingUserId);

                if (_maxMessageLengthSetting > 0 && command.Content.Length > _maxMessageLengthSetting)
                {
                    throw new Exception(String.Format(LanguageResources.SendMessageTooLong, _maxMessageLengthSetting));
                }

                var room = _roomRepository.VerifyRoom(command.TargetRoomId);

                //room.EnsureUserIsInRoom();

                if (room == null || (room.PrivateRoom && room.AllowedUsers.None(c => c == callingUser.Id)))
                {
                    return;
                }

                room.EnsureOpen();

                var lastActivity = _clock.UtcNow;

                callingUser.UpdateLastActivity(lastActivity);

                var message = new Message(Guid.NewGuid(), command.TargetRoomId, command.CallingUserId, command.Content, lastActivity);

                _messageRepository.Add(message);

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