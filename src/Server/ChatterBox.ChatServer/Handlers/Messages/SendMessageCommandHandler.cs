using System;
using System.Linq;
using System.Threading.Tasks;
using ChatterBox.ChatServer.ConfigurationSettings;
using ChatterBox.Core.Infrastructure;
using ChatterBox.Core.Persistence;
using ChatterBox.Domain.Aggregates.MessageAggregate;
using ChatterBox.Domain.Aggregates.RoomAggregate;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.Domain.Extensions;
using ChatterBox.Domain.Properties;
using ChatterBox.MessageContracts.Commands;
using Nimbus;
using ThirdDrawer.Extensions.CollectionExtensionMethods;

namespace ChatterBox.ChatServer.Handlers.Messages
{
    public class SendMessageCommandHandler : ScopedUserCommandHandler<SendMessageCommand>
    {
        private readonly IClock _clock;
        private readonly MaxMessageLengthSetting _maxMessageLengthSetting;

        public SendMessageCommandHandler(
            Func<IUnitOfWork> unitOfWork,
            IBus bus,
            IClock clock,
            MaxMessageLengthSetting maxMessageLengthSetting) 
            : base(unitOfWork, bus)
        {
            _clock = clock;
            _maxMessageLengthSetting = maxMessageLengthSetting;
        }

        public override async Task Execute(IUnitOfWork context, User callingUser, SendMessageCommand command)
        {
            if (_maxMessageLengthSetting > 0 && command.Content.Length > _maxMessageLengthSetting)
            {
                throw new Exception(String.Format(LanguageResources.SendMessageTooLong, _maxMessageLengthSetting));
            }

            var room = context.Repository<Room>().VerifyRoom(command.TargetRoomId);

            //room.EnsureUserIsInRoom();

            if (room == null || (room.Private && room.AllowedUsers.None(c => c == callingUser.Id)))
            {
                return;
            }

            room.EnsureOpen();

            var lastActivity = _clock.UtcNow;

            callingUser.UpdateLastActivity(lastActivity);

            var repository = context.Repository<Message>();

            var message = new Message(command.TargetRoomId, command.UserId, command.Content, lastActivity);

            repository.Add(message);

            context.Complete();
        }
    }
}