﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac;
using Autofac.Builder;
using ChatterBox.Core.Infrastructure;
using ChatterBox.Core.Infrastructure.Facts;
using ChatterBox.Core.Persistence;
using ChatterBox.Core.Persistence.Memory;
using ChatterBox.Domain.Aggregates.RoomAggregate.Facts;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.Domain.Aggregates.UserAggregate.Facts;
using ChatterBox.MessageContracts.Commands;
using ChatterBox.MessageContracts.Requests;
using Shouldly;

namespace ChatterBox.ChatServer.IntegrationTests.Scenarios
{
    public class WithEmptyFactStoreFirstAuthentication : ChatServerSpecificationForBus
    {
        [Then]
        public async Task ShouldCreateUserAsAdministrator()
        {
            var response = await Subject.Request(new AuthenticateUserRequest("fred@rockwell.com", "yabadabado"));
            response.User.UserRole.ShouldBe((int)UserRole.Admin);
        }
    }
    
    public class When1 : ChatServerSpecificationForBus
    {
        protected override IContainer CreateContainer()
        {
            var userCreatedFact = new UserCreatedFact
            {
                AggregateRootId = Guid.Parse("95cdcb0c-aad2-438d-b964-a5beb6c9f43b"),
                Name = "fred@rockwell.com",
                Email = string.Empty,
                Hash = null,
                Salt = "LTkGikACIlLJptwW6Wmrnw==",
                HashedPassword = "f1c7764c8b6293e2a626689f7d460eb344cd5242ee5e507c877e2b4a17049627",
                UserRole = UserRole.Admin,
                LastActivity = DateTimeHelper.UtcNow,
                Status = UserStatus.Active,
            };
            userCreatedFact.SetUnitOfWorkProperties(new UnitOfWorkProperties(Guid.Parse("9e5bf6b9-d545-40f5-bfc2-ab27722bb190"), 0, DateTimeHelper.UtcNow));

            var roomCreatedFact = new RoomCreatedFact
            {
                AggregateRootId = Guid.Parse("51caa0fe-2156-492f-b690-e1ad1befc2ad"),
                Name = "Home",
                OwnerId = Guid.Parse("95cdcb0c-aad2-438d-b964-a5beb6c9f43b")
            };
            roomCreatedFact.SetUnitOfWorkProperties(new UnitOfWorkProperties(Guid.Parse("63113645-ac0a-4dcd-a206-f939219d2dcc"), 0, DateTimeHelper.UtcNow));

            var factStore = new MemoryFactStore();
            factStore.ImportFrom(new List<IFact> { userCreatedFact, roomCreatedFact });

            return IoC.LetThereBeIoC(ContainerBuildOptions.None, builder =>
            {
                builder.RegisterInstance(factStore)
                    .As<IFactStore>()
                    .SingleInstance();
            });
        }

        [Then]
        public async Task Monkey()
        {
            var response = await Subject.Request(new AuthenticateUserRequest("fred@rockwell.com", "yabadabado"));
            response.User.LastActivity.ShouldNotBe(DateTimeOffset.Parse("2014-04-06T08:37:56.000631+00:00"));
        }

        //public async Task CorrectlyCreateRoom()
        //{
        //    var response = await Subject.Request(new CreateRoomRequest("Rockwell", Guid.Parse("95cdcb0c-aad2-438d-b964-a5beb6c9f43b")));
        //    response.RoomId.ShouldNotBe(Guid.Empty);
        //}
        
        [Then]
        public async Task SendMessage()
        {
            await Subject.Send(new SendMessageCommand("Hello World!", Guid.Parse("51caa0fe-2156-492f-b690-e1ad1befc2ad"), Guid.Parse("95cdcb0c-aad2-438d-b964-a5beb6c9f43b")));
        }
    }
}