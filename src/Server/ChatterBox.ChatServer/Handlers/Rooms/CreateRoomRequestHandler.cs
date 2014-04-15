using System;
using System.Threading.Tasks;
using Autofac.Features.OwnedInstances;
using ChatterBox.Core.Persistence;
using ChatterBox.Domain.Aggregates.RoomAggregate;
using ChatterBox.Domain.Extensions;
using ChatterBox.MessageContracts.Requests;
using ThirdDrawer.Extensions.StringExtensionMethods;

namespace ChatterBox.ChatServer.Handlers.Rooms
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

            var targetRoom = roomRepository.VerifyRoom(request.UserId);

            if (targetRoom != null)
            {
                if (!targetRoom.Closed)
                {
                    throw new Exception("{0} already exists.".FormatWith(request.RoomName));
                }
                else
                {
                    throw new Exception("{0} already exists, but it's closed.".FormatWith(request.RoomName));
                }
            }

            var room = new Room(request.RoomName, request.UserId);

            roomRepository.Add(room);

            context.Complete();

            return new CreateRoomResponse(room.Id);
        }
    }
}