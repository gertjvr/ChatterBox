using ChatterBox.ChatServer.Infrastructure.Mappers;
using ChatterBox.Core.Tests.Specifications;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.MessageContracts.Dtos;
using Ploeh.AutoFixture;
using Shouldly;

namespace ChatterBox.ChatServer.Tests.Mappings
{
    public class WhenMappingUserToUserDto : AutoSpecificationFor<UserToUserDtoMapper>
    {
        public UserDto Expected { get; private set; }

        public User User { get; private set; }

        protected override UserToUserDtoMapper Given()
        {
            User = Fixture.Create<User>();

            return Fixture.Create<UserToUserDtoMapper>();
        }

        protected override void When()
        {
            Expected = Subject.Map(User);
        }

        [Then]
        public void ShouldHaveMappedCorrectly()
        {
            Expected.Name.ShouldBe(User.Name);
            Expected.Hash.ShouldBe(User.Hash);
            Expected.LastActivity.ShouldBe(User.LastActivity);
            Expected.Status.ShouldBe((int)User.Status);
            Expected.Role.ShouldBe((int)User.Role);
        }
    }
}