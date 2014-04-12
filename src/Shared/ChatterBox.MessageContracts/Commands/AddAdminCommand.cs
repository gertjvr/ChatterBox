using System;

namespace ChatterBox.MessageContracts.Commands
{
    public class AddAdminCommand : IUserCommand
    {
        public AddAdminCommand(Guid targetUserId, Guid userId)
        {
            TargetUserId = targetUserId;
        }

        public Guid TargetUserId { get; set; }

        public Guid UserId { get; protected set; }
    }
}