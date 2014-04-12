using System;
using System.Threading.Tasks;
using Autofac.Features.OwnedInstances;
using ChatterBox.Core.Mapping;
using ChatterBox.Core.Persistence;
using ChatterBox.Domain.Aggregates.RoomAggregate;
using ChatterBox.MessageContracts.Dtos;
using ChatterBox.MessageContracts.Requests;

namespace ChatterBox.ChatServer.Handlers
{
    public class RoomInfoRequestHandler : ScopedRequestHandler<RoomInfoRequest, RoomInfoResponse>
    {
        private readonly IMapToNew<Room, RoomDto> _roomMapper;

        public RoomInfoRequestHandler(
            Func<Owned<IUnitOfWork>> unitOfWork,
            IMapToNew<Room, RoomDto> roomMapper) 
            : base(unitOfWork)
        {
            _roomMapper = roomMapper;
        }

        public override async Task<RoomInfoResponse> Execute(IUnitOfWork context, RoomInfoRequest request)
        {
            var roomRepository = context.Repository<Room>();

            var room = roomRepository.GetById(request.RoomId);

            return new RoomInfoResponse(_roomMapper.Map(room));
        }
    }
}