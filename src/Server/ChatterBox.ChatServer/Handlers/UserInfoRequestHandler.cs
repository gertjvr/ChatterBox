using System;
using System.Threading.Tasks;
using Autofac.Features.OwnedInstances;
using ChatterBox.Core.Mapping;
using ChatterBox.Core.Persistence;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.MessageContracts.Dtos;
using ChatterBox.MessageContracts.Requests;

namespace ChatterBox.ChatServer.Handlers
{
    public class UserInfoRequestHandler : ScopedRequestHandler<UserInfoRequest, UserInfoResponse>
    {
        private readonly IMapToNew<User, UserDto> _mapper;

        public UserInfoRequestHandler(
            Func<Owned<IUnitOfWork>> unitOfWork,
            IMapToNew<User, UserDto> mapper) 
            : base(unitOfWork)
        {
            _mapper = mapper;
        }

        public override async Task<UserInfoResponse> Execute(IUnitOfWork context, UserInfoRequest request)
        {
            var repository = context.Repository<User>();

            var user = repository.GetById(request.UserId);

            return new UserInfoResponse(_mapper.Map(user));
        }
    }
}