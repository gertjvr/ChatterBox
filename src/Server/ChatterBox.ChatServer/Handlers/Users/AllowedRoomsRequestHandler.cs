using System;
using System.Linq;
using System.Threading.Tasks;
using ChatterBox.Core.Infrastructure;
using ChatterBox.Core.Mapping;
using ChatterBox.Domain.Aggregates.RoomAggregate;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.Domain.Extensions;
using ChatterBox.MessageContracts.Dtos;
using ChatterBox.MessageContracts.Users.Requests;
using Nimbus.Handlers;

namespace ChatterBox.ChatServer.Handlers.Users
{
    public class AllowedRoomsRequestHandler : IHandleRequest<AllowedRoomsRequest, AllowedRoomsResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Room> _roomRepository;
        private readonly IMapToNew<Room, RoomDto> _mapper;

        public AllowedRoomsRequestHandler(
            IUnitOfWork unitOfWork,
            IRepository<User> userRepository,
            IRepository<Room> roomRepository, 
            IMapToNew<Room, RoomDto> mapper)
        {
            if (unitOfWork == null) 
                throw new ArgumentNullException("unitOfWork");
            
            if (userRepository == null) 
                throw new ArgumentNullException("userRepository");
            
            if (roomRepository == null) 
                throw new ArgumentNullException("roomRepository");
            
            if (mapper == null) 
                throw new ArgumentNullException("mapper");

            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _roomRepository = roomRepository;
            _mapper = mapper;
        }

        public async Task<AllowedRoomsResponse> Handle(AllowedRoomsRequest request)
        {
            try
            {
                var callingUser = _userRepository.VerifyUser(request.CallingUserId);
                var targetUser = _userRepository.VerifyUser(request.TargetUserId);

                var results = _roomRepository.GetAllowedRoomsByUserId(targetUser);

                var response = new AllowedRoomsResponse(results.Select(_mapper.Map).ToArray());

                _unitOfWork.Complete();

                return response;
            }
            catch
            {
                _unitOfWork.Abandon();
                throw;
            }
        }
    }
}