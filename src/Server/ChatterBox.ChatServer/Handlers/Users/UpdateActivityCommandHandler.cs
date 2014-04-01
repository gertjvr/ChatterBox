using System;
using System.Threading.Tasks;
using ChatterBox.MessageContracts.Users.Commands;
using Nimbus.Handlers;

namespace ChatterBox.ChatServer.Handlers.Users
{
    public class UpdateActivityCommandHandler : IHandleCommand<UpdateActivityCommand>
    {
        public Task Handle(UpdateActivityCommand busCommand)
        {
            throw new NotImplementedException();
        }
    }
}