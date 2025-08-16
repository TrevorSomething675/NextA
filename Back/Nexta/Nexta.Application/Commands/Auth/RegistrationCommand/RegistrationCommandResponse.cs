using Nexta.Application.DTO.Response;

namespace Nexta.Application.Commands.Auth.RegistrationCommand
{
    public class RegistrationCommandResponse(UserResponse user)
    {
		public UserResponse User { get; init; } = user;
	}
}