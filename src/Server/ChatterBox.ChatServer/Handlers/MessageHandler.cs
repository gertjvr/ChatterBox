using System.Threading.Tasks;
using ChatterBox.MessageContracts.Commands;
using ChatterBox.MessageContracts.Events;
using Nimbus;
using Nimbus.Handlers;

namespace ChatterBox.ChatServer.Handlers
{
    public class MessageHandler : IHandleCommand<BroadcastMessageCommand>
    {
        private readonly IBus _bus;

        public MessageHandler(IBus bus)
        {
            _bus = bus;
        }

        public async Task Handle(BroadcastMessageCommand message)
        {
            await _bus.Publish(new MessageReceivedEvent(message.ClientId, message.Message));
        }
    }
}