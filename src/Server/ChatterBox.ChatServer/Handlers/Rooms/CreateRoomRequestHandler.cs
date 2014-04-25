using System;
using System.Threading.Tasks;
using ChatterBox.Core.Infrastructure;
using ChatterBox.Domain.Aggregates.RoomAggregate;
using ChatterBox.Domain.Aggregates.UserAggregate;
using ChatterBox.Domain.Extensions;
using ChatterBox.MessageContracts.Rooms.Requests;
using Nimbus.Handlers;
using ThirdDrawer.Extensions.StringExtensionMethods;

namespace ChatterBox.ChatServer.Handlers.Rooms
{
    public class CreateRoomRequestHandler : IHandleRequest<CreateRoomRequest, CreateRoomResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Room> _roomRepository;

        public CreateRoomRequestHandler(
            IUnitOfWork unitOfWork,
            IRepository<User> userRepository,
            IRepository<Room> roomRepository)
        {
            if (unitOfWork == null) 
                throw new ArgumentNullException("unitOfWork");
            
            if (userRepository == null) 
                throw new ArgumentNullException("userRepository");
            
            if (roomRepository == null) 
                throw new ArgumentNullException("roomRepository");

            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _roomRepository = roomRepository;
        }

        public async Task<CreateRoomResponse> Handle(CreateRoomRequest request)
        {
            if (request == null) 
                throw new ArgumentNullException("request");

            try
            {
                var callingUser = _userRepository.VerifyUser(request.CallingUserId);
                var targetRoom = _roomRepository.GetByName(request.RoomName);

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

                var room = new Room(request.RoomName, request.CallingUserId, string.Empty, string.Empty, false);

                _roomRepository.Add(room);

                var response = new CreateRoomResponse(room.Id);

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