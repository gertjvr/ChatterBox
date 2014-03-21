using System.Diagnostics;
using System.Threading.Tasks;
using ChatterBox.MessageContracts.Commands;
using ChatterBox.MessageContracts.Events;
using Nimbus;
using Nimbus.Handlers;
using Serilog;
using Serilog.Events;

namespace ChatterBox.ChatServer.Handlers
{
    public class BroadcastMessageHandler : IHandleCommand<BroadcastMessageCommand>
    {
        private readonly IBus _bus;

        public BroadcastMessageHandler(IBus bus)
        {
            _bus = bus;
        }

        public async Task Handle(BroadcastMessageCommand message)
        {
            Log.Debug("{message}", message);

            await _bus.Publish(new MessageReceivedEvent(message.ClientId, message.Message));
        }
    }
}