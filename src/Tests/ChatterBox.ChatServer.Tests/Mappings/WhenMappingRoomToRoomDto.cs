using System.Linq;
using ChatterBox.ChatServer.Infrastructure.Mappers;
using ChatterBox.Core.Infrastructure;
using ChatterBox.Core.Mapping;
using ChatterBox.Core.Tests.Specifications;
using ChatterBox.Domain.Aggregates.MessageAggregate;
using ChatterBox.Domain.Aggregates.RoomAggregate;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.Domain.Extensions;
using ChatterBox.MessageContracts.Dtos;
using Ploeh.AutoFixture;
using Shouldly;

namespace ChatterBox.ChatServer.Tests.Mappings
{
    public class WhenMappingRoomToRoomDto : AutoSpecificationFor<RoomToRoomDtoMapper>
    {
        public MessageToMessageDtoMapper MessageMapper { get; private set; }
        public UserToUserDtoMapper UserMapper { get; private set; }
        public IRepository<Message> MessageRepository { get; private set; }
        public IRepository<User> UserRepository { get; private set; }

        public RoomDto Expected { get; private set; }
        public Room Room { get; private set; }

        protected override RoomToRoomDtoMapper Given()
        {
            Room = Fixture.Create<Room>();

            UserMapper = Fixture.Create<UserToUserDtoMapper>();
            MessageMapper = Fixture.Create<MessageToMessageDtoMapper>();

            UserRepository = Fixture.Create<IRepository<User>>();
            MessageRepository = Fixture.Create<IRepository<Message>>();

            Fixture.Inject<IMapToNew<User, UserDto>>(UserMapper);
            Fixture.Inject<IMapToNew<Message, MessageDto>>(MessageMapper);

            return Fixture.Create<RoomToRoomDtoMapper>();
        }

        protected override void When()
        {
            Expected = Subject.Map(Room);
        }

        [Then]
        public void ShouldHaveMappedCorrectly()
        {
            Expected.Name.ShouldBe(Room.Name);
            Expected.Closed.ShouldBe(Room.Closed);
            Expected.PrivateRoom.ShouldBe(Room.PrivateRoom);
            Expected.Topic.ShouldBe(Room.Topic);
            Expected.WelcomeMessage.ShouldBe(Room.WelcomeMessage);
            Expected.Users.ShouldBe(Room.Users.Select(userId => UserMapper.Map(UserRepository.GetById(userId))).ToArray());
            Expected.Owners.ShouldBe(Room.Owners.Select(userId => UserMapper.Map(UserRepository.GetById(userId))).ToArray());
            Expected.RecentMessages.ShouldBe(MessageRepository.GetMessages().Select(MessageMapper.Map));
        }
    }
}