using System;
using System.Threading.Tasks;
using ChatterBox.MessageContracts.Commands;
using Nimbus.Handlers;

namespace ChatterBox.ChatServer.Handlers
{
    public class UpdateActivityCommandHandler : IHandleCommand<UpdateActivityCommand>
    {
        public Task Handle(UpdateActivityCommand busCommand)
        {
            throw new NotImplementedException();
        }
    }
}