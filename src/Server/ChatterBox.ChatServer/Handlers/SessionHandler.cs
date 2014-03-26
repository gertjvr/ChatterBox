using System.Threading.Tasks;
using ChatterBox.Core;
using ChatterBox.Core.Infrastructure;
using ChatterBox.MessageContracts.Commands;
using ChatterBox.MessageContracts.Events;
using Nimbus;
using Nimbus.Handlers;

namespace ChatterBox.ChatServer.Handlers
{
    public class SessionHandler : IHandleCommand<ConnectCommand>, IHandleCommand<DisconnectCommand>
    {
        private readonly IBus _bus;
        private readonly IClock _clock;

        public SessionHandler(IBus bus, IClock clock)
        {
            _bus = bus;
            _clock = clock;
        }

        public async Task Handle(ConnectCommand message)
        {
            await _bus.Publish(new ClientConnectedEvent(message.ClientId, _clock.UtcNow));
        }

        public async Task Handle(DisconnectCommand message)
        {
            await _bus.Publish(new ClientDisconnectedEvent(message.ClientId, _clock.UtcNow));
        }
    }
}