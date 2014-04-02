﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Features.OwnedInstances;
using ChatterBox.Core.Persistence;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.Domain.Queries;
using ChatterBox.MessageContracts.Commands;
using ThirdDrawer.Extensions.StringExtensionMethods;

namespace ChatterBox.ChatServer.Handlers
{
    public class ChangeUserNameCommandHandler : ScopedCommandHandler<ChangeUserNameCommand>
    {
        public ChangeUserNameCommandHandler(Func<Owned<IUnitOfWork>> unitOfWorkFactory) 
            : base(unitOfWorkFactory)
        {
        }

        public override async Task Execute(IUnitOfWork context, ChangeUserNameCommand command)
        {
            var repository = context.Repository<User>();

            EnsureUserNameIsAvailible(repository, command.NewUserName);

            var user = repository.GetById(command.UserId);
            user.ChangeUserName(command.NewUserName);

            context.Complete();
        }

        private void EnsureUserNameIsAvailible(IRepository<User> repository, string userName)
        {
            if (repository.Query(new EnsureUserNameIsAvailibleQuery(userName)).Any())
                throw new InvalidOperationException("Username {0} already taken.".FormatWith(userName));
        }
    }
 }