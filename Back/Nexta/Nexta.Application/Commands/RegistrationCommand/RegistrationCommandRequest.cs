using MediatR;

namespace Nexta.Application.Commands.RegistrationCommand
{
    public class RegistrationCommandRequest : IRequest<RegistrationCommandResponse>
    {
		public string Email { get; set; } = null!;

		public string FirstName { get; set; } = null!;
		public string MiddleName { get; set; } = null!;
		public string LastName { get; set; } = null!;
        public string Password { get; set; } = null!;
		public string ConfirmPassword { get; set; } = null!;
    }
}