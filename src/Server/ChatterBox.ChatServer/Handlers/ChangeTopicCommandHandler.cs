using System;
using System.Threading.Tasks;
using ChatterBox.MessageContracts.Commands;
using Nimbus.Handlers;

namespace ChatterBox.ChatServer.Handlers
{
    public class ChangeTopicCommandHandler : IHandleCommand<ChangeTopicCommand>
    {
        public Task Handle(ChangeTopicCommand busCommand)
        {
            throw new NotImplementedException();
        }
    }
}