using Nexta.Application.DTO.Response;

namespace Nexta.Application.Commands.Auth.RegisterCommand
{
    public class RegisterCommandResponse(UserResponse user, string accessToken)
    {
		public UserResponse User { get; init; } = user;
        public string AccessToken { get; init; } = accessToken;
	}
}