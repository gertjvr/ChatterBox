using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts
{
    public interface IUserBusCommand : IBusCommand
    {
        Guid UserId { get; } 
    }
}