using Nexta.Application.DTO.Users;
using MediatR;

namespace Nexta.Application.Commands.Auth.LoginCommand
{
    public class LoginCommand : IRequest<UserDto>
    {
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}