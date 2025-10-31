namespace Nexta.Application.Commands.Auth.VerifyCodeCommand
{
    public record VerifyCodeCommandResponse
    {
        public Guid Id { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string? MiddleName { get; init; }
        public string Email { get; init; }
        public string Role { get; init; }

        public string AccessToken { get; init; }
    }
}