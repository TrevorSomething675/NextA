namespace Nexta.Web.Models.Account
{
    public class ChangePasswordCommandRequest
    {
        public Guid UserId { get; init; }
        public string Email { get; init; }
        public string LegacyPassword { get; init; }
        public string Password { get; init; }
        public string ConfirmPassword { get; init; }
    }
}