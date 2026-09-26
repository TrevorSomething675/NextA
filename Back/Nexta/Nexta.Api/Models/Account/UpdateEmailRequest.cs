namespace Nexta.Web.Models.Account
{
    public class UpdateEmailRequest
    {
        public string Email { get; set; }
        public string LegacyEmail { get; set; }
        public string Code { get; set; }
    }
}