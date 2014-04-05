using System;
using System.Threading.Tasks;
using ChatterBox.Core.Persistence;
using Nimbus.Handlers;
using Nimbus.MessageContracts;

namespace ChatterBox.ChatServer
{
    public abstract class ScopedCommandHandler<TBusCommand> : IHandleCommand<TBusCommand> where TBusCommand : IBusCommand
    {
        private readonly Func<IUnitOfWork> _unitOfWork;

        public ScopedCommandHandler(Func<IUnitOfWork> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(TBusCommand command)
        {
            using (var context = _unitOfWork())
            {
                try
                {
                    await Execute(context, command);
                }
                catch
                {
                    context.Abandon();
                    throw;
                }
            }
        }

        public abstract Task Execute(IUnitOfWork context, TBusCommand command);
    }
}