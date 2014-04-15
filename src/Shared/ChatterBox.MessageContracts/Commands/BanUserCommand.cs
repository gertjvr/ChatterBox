using System;

namespace ChatterBox.MessageContracts.Commands
{
    public class BanUserCommand : IUserBusCommand
    {
        public BanUserCommand(Guid targetUserId, Guid userId)
        {
            TargetUserId = targetUserId;
            UserId = userId;
        }

        public Guid TargetUserId { get; protected set; }

        public Guid UserId { get; protected set; }
    }
}