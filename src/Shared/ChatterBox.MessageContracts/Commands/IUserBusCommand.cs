using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Commands
{
    public interface IUserBusCommand : IBusCommand
    {
        Guid UserId { get; } 
    }
}