using MediatR;

namespace Nexta.Application.Commands.Account.UpdateEmailCommand
{
    public class UpdateEmailCommand : IRequest<UpdateEmailCommandResponse>
    {
        public string Email { get; set; }
        public string LegacyEmail { get; set; }
        public string Code { get; set; }
    }
}
