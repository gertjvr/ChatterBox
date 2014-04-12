using System;
using System.Threading.Tasks;
using ChatterBox.Core.Persistence;
using ChatterBox.Domain.Aggregates.RoomAggregate;
using ChatterBox.MessageContracts.Commands;

namespace ChatterBox.ChatServer.Handlers
{
    public class AddOwnerCommandHandler : ScopedCommandHandler<AddOwnerCommand>
    {
        public AddOwnerCommandHandler(
            Func<IUnitOfWork> unitOfWork) 
            : base(unitOfWork)
        {
        }

        public override async Task Execute(IUnitOfWork context, AddOwnerCommand command)
        {
            var repository = context.Repository<Room>();

            var room = repository.GetById(command.RoomId);

            room.AddOwner(command.UserId);

            context.Complete();
        }
    }
}