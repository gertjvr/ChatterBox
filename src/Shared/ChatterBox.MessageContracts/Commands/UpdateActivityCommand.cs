﻿using System;
using Nimbus.MessageContracts;

namespace ChatterBox.MessageContracts.Commands
{
    public class UpdateActivityCommand : IBusCommand
    {
        public Guid UserId { get; set; }

        public string ClientId { get; set; }
        
        public string UserAgent  { get; set; }
    }
}