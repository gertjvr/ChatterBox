using System;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Features.OwnedInstances;
using ChatterBox.Core.Mapping;
using ChatterBox.Core.Persistence;
using ChatterBox.Domain.Aggregates.RoomAggregate;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.Domain.Extensions;
using ChatterBox.Domain.Queries;
using ChatterBox.MessageContracts.Dtos;
using ChatterBox.MessageContracts.Users.Requests;

namespace ChatterBox.ChatServer.Handlers.Users
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
            var user = context.Repository<User>().VerifyUser(request.UserId);

            var repository = context.Repository<Room>();

            var rooms = repository.Query(new UserAllowedRoomsQuery(user.Id));

            return new AllowedRoomsResponse(rooms.Select(_mapper.Map).ToArray());
        }
    }
}