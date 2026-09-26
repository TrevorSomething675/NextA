namespace Nexta.Web.Areas.Models
{
    public class AccessRecoveryRequest
    {
        public string Email { get; set; }
        public string Code { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
