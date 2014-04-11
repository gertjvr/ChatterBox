using System;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Features.OwnedInstances;
using ChatterBox.Core.Mapping;
using ChatterBox.Core.Persistence;
using ChatterBox.Domain.Aggregates.RoomAggregate;
using ChatterBox.Domain.Queries;
using ChatterBox.MessageContracts.Dtos;
using ChatterBox.MessageContracts.Requests;

namespace ChatterBox.ChatServer.Handlers
{
    public class AllowedRoomsRequestHandler : ScopedRequestHandler<AllowedRoomsRequest, AllowedRoomsResponse>
    {
        private readonly IMapToNew<Room, RoomDto> _mapper;

        public AllowedRoomsRequestHandler(
            Func<Owned<IUnitOfWork>> unitOfWork,
            IMapToNew<Room, RoomDto> mapper) 
            : base(unitOfWork)
        {
            _mapper = mapper;
        }

        public override async Task<AllowedRoomsResponse> Execute(IUnitOfWork context, AllowedRoomsRequest request)
        {
            var repository = context.Repository<Room>();

            var rooms = repository.Query(new UserAllowedRoomsQuery(request.UserId));

            return new AllowedRoomsResponse(rooms.Select(_mapper.Map).ToArray());
        }
    }
}