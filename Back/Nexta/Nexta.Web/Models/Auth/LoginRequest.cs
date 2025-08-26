namespace Nexta.Web.Models.Auth
{
    public class LoginRequest
    {
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
