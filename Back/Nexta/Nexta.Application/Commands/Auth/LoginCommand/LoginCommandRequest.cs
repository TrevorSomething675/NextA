﻿using MediatR;

namespace Nexta.Application.Commands.Auth.LoginCommand
{
    public class LoginCommandRequest : IRequest<LoginCommandResponse>
    {
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}