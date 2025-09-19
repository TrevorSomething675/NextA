namespace Nexta.Web.Models.Account
{
    public class ChangePasswordRequest
    {
        public Guid UserId { get; init; }
        public string Email { get; init; }
        public string LegacyPassword { get; init; }
        public string Password { get; init; }
        public string ConfirmPassword { get; init; }
    }
}