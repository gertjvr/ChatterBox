using System;
using ChatterBox.MessageContracts.Dtos;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Authentication.Request
{
    public class AuthenticateUserResponse : IBusResponse
    {
        protected AuthenticateUserResponse()
        {   
        }

        public AuthenticateUserResponse(UserDto user, RoomDto[] rooms, Guid userId)
        {
            if (user == null) 
                throw new ArgumentNullException("user");

            if (rooms == null) 
                throw new ArgumentNullException("rooms");
            
            if (userId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "userId");

            User = user;
            Rooms = rooms;
            UserId = userId;
        }

        public UserDto User { get; private set; }

        public RoomDto[] Rooms { get; private set; }

        public Guid UserId { get; private set; }
    }
}