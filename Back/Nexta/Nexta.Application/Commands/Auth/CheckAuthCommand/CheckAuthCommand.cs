using MediatR;

namespace Nexta.Application.Commands.Auth.CheckAuthCommand
{
    public class CheckAuthCommand : IRequest<CheckAuthCommandResponse>
    {
        public string Email { get; set; }
        public string Role { get; set; }
    }
}