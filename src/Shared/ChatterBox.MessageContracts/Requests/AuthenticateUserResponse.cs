using System;
using ChatterBox.MessageContracts.Dtos;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Requests
{
    public class AuthenticateUserResponse : IBusResponse
    {
        protected AuthenticateUserResponse()
        {
        }

        public AuthenticateUserResponse(
            UserDto user,
            RoomDto[] rooms,
            Guid clientId,
            Guid userId)
        {
            User = user;
            Rooms = rooms;
            ClientId = clientId;
            UserId = userId;
        }

        public UserDto User { get; set; }

        public RoomDto[] Rooms { get; set; }

        public Guid ClientId { get; set; }

        public Guid UserId { get; set; }

        public static AuthenticateUserResponse Failed()
        {
            return new AuthenticateUserResponse
            {
                User = null,
                Rooms = new RoomDto[0],
                ClientId = Guid.Empty,
                UserId = Guid.Empty,
            };
        }
    }
}