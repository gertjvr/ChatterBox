using System;
using System.Threading.Tasks;
using ChatterBox.Core.Infrastructure;
using ChatterBox.Core.Mapping;
using ChatterBox.Domain.Aggregates.RoomAggregate;
using ChatterBox.Domain.Extensions;
using ChatterBox.MessageContracts.Dtos;
using ChatterBox.MessageContracts.Rooms.Requests;
using Nimbus.Handlers;

namespace ChatterBox.ChatServer.Handlers.Rooms
{
    public class RoomInfoRequestHandler : IHandleRequest<RoomInfoRequest, RoomInfoResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Room> _roomRepository;
        private readonly IMapToNew<Room, RoomDto> _roomMapper;

        public RoomInfoRequestHandler(
            IUnitOfWork unitOfWork,
            IRepository<Room> roomRepository, 
            IMapToNew<Room, RoomDto> roomMapper)
        {
            if (unitOfWork == null) 
                throw new ArgumentNullException("unitOfWork");
            
            if (roomRepository == null) 
                throw new ArgumentNullException("roomRepository");
            
            if (roomMapper == null) 
                throw new ArgumentNullException("roomMapper");

            _unitOfWork = unitOfWork;
            _roomRepository = roomRepository;
            _roomMapper = roomMapper;
        }

        public async Task<RoomInfoResponse> Handle(RoomInfoRequest request)
        {
            try
            {
                var targetRoom = _roomRepository.VerifyRoom(request.TargetRoomId);

                return new RoomInfoResponse(_roomMapper.Map(targetRoom));
            }
            catch
            {
                _unitOfWork.Abandon();
                throw;
            }
        }
    }
}