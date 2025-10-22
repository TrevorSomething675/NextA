using Nexta.Application.DTO.User;

namespace Nexta.Application.Commands.Auth.RegisterCommand
{
    public class RegisterCommandResponse(UserDto user, string accessToken)
    {
		public UserDto User { get; init; } = user;
        public string AccessToken { get; init; } = accessToken;
	}
}