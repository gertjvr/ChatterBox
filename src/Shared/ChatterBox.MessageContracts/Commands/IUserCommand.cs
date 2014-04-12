using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Commands
{
    public interface IUserCommand : IBusCommand
    {
        Guid UserId { get; } 
    }
}