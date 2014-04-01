using System;
using System.Threading.Tasks;
using ChatterBox.MessageContracts.Owners.Commands;
using Nimbus.Handlers;

namespace ChatterBox.ChatServer.Handlers.Owners
{
    public class ChangeTopicCommandHandler : IHandleCommand<ChangeTopicCommand>
    {
        public Task Handle(ChangeTopicCommand busCommand)
        {
            throw new NotImplementedException();
        }
    }
}