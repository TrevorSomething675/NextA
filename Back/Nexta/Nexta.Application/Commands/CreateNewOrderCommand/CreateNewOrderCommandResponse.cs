﻿using Nexta.Domain.Models;

namespace Nexta.Application.Commands.CreateNewOrderCommand
{
    public class CreateNewOrderCommandResponse(Order order)
    {
        public Order Order { get; set; } = order;
    }
}