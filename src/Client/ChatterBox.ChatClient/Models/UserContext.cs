using System;

namespace ChatterBox.ChatClient.Models
{
    public class UserContext
    {
        public Guid UserId { get; private set; }
        public User User { get; private set; }

        public void SetUserId(Guid userId, User user)
        {
            UserId = userId;
            User = user;
        }
    }
}