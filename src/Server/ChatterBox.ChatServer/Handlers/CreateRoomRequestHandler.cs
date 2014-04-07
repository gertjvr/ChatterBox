using System;
using System.Threading.Tasks;
using Autofac.Features.OwnedInstances;
using ChatterBox.Core.Persistence;
using ChatterBox.Domain.Aggregates.RoomAggregate;
using ChatterBox.MessageContracts.Requests;

namespace ChatterBox.ChatServer.Handlers
{
    public class CreateRoomRequestHandler : ScopedRequestHandler<CreateRoomRequest, CreateRoomResponse>
    {
        public CreateRoomRequestHandler(Func<Owned<IUnitOfWork>> unitOfWork) 
            : base(unitOfWork)
        {
        }

        public override async Task<CreateRoomResponse> Execute(IUnitOfWork context, CreateRoomRequest request)
        {
            var roomRepository = context.Repository<Room>();

            var room = new Room(request.RoomName, request.UserId);

            roomRepository.Add(room);

            context.Complete();

            return new CreateRoomResponse(room.Id);
        }
    }
}