using System;

namespace ChatterBox.MessageContracts.Admins.Commands
{
    public class BanUserCommand : IUserBusCommand
    {
        protected BanUserCommand()
        {   
        }
        
        public BanUserCommand(Guid targetUserId, Guid userId)
        {
            if (targetUserId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "targetUserId");

            if (userId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "userId");

            TargetUserId = targetUserId;
            UserId = userId;
        }

        public Guid TargetUserId { get; private set; }

        public Guid UserId { get; private set; }
    }
}