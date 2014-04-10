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

        public UserDto User { get; protected set; }

        public RoomDto[] Rooms { get; protected set; }

        public Guid ClientId { get; protected set; }

        public Guid UserId { get; protected set; }

        public static AuthenticateUserResponse Failed()
        {
            return new AuthenticateUserResponse(
                null, 
                new RoomDto[0], 
                Guid.Empty, 
                Guid.Empty);
        }
    }
}