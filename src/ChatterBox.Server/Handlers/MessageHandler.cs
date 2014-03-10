using System.Threading.Tasks;
using ChatterBox.MessageContracts.Commands;
using ChatterBox.MessageContracts.Events;
using Nimbus;
using Nimbus.Handlers;

namespace ChatterBox.Server.Handlers
{
    public class MessageHandler : IHandleCommand<SendMessageCommand>
    {
        private readonly IBus _bus;

        public MessageHandler(IBus bus)
        {
            _bus = bus;
        }

        public async Task Handle(SendMessageCommand message)
        {
            await _bus.Publish(new MessageReceivedEvent(message.ClientId, message.Message));
        }
    }
}