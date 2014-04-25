using ChatterBox.ChatServer.Infrastructure.Mappers;
using ChatterBox.Core.Infrastructure;
using ChatterBox.Core.Mapping;
using ChatterBox.Core.Tests.Specifications;
using ChatterBox.Domain.Aggregates.MessageAggregate;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.MessageContracts.Dtos;
using NSubstitute;
using Ploeh.AutoFixture;
using Shouldly;

namespace ChatterBox.ChatServer.Tests.Mappings
{
    public class WhenMappingMessageToMessageDto : AutoSpecificationFor<MessageToMessageDtoMapper>
    {
        public UserToUserDtoMapper UserMapper { get; private set; }
        public IRepository<Message> MessageRepository { get; private set; }
        public IRepository<User> UserRepository { get; private set; }

        public MessageDto Expected { get; private set; }
        public User User { get; set; }
        public Message Message { get; private set; }

        protected override MessageToMessageDtoMapper Given()
        {
            User = Fixture.Create<User>();
            Message = Fixture.Create<Message>();

            UserMapper = Fixture.Create<UserToUserDtoMapper>();

            UserRepository = Fixture.Freeze<IRepository<User>>();
            UserRepository.GetById(Arg.Is(Message.UserId))
                .Returns(User);

            MessageRepository = Fixture.Freeze<IRepository<Message>>();

            Fixture.Inject<IMapToNew<User, UserDto>>(UserMapper);

            return Fixture.Create<MessageToMessageDtoMapper>();
        }

        protected override void When()
        {
            Expected = Subject.Map(Message);
        }

        [Then]
        public void ShouldHaveMappedCorrectly()
        {
            Expected.Id.ShouldBe(Message.Id);
            Expected.Content.ShouldBe(Message.Content);
            Expected.CreatedAt.ShouldBe(Message.CreatedAt);
            Expected.User.Hash.ShouldBe(UserMapper.Map(UserRepository.GetById(Message.UserId)).Hash);
        }
    }
}