using Nexta.Domain.Models.DataModels;
using MediatR;

namespace Nexta.Application.Commands.LoginCommand
{
    public class LoginCommandRequest : IRequest<Result<LoginCommandResponse>>
    {
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}