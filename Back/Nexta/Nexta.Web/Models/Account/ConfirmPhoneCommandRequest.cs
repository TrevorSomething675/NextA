namespace Nexta.Web.Models.Account
{
    public class ConfirmPhoneCommandRequest
    {
        public string Email { get; init; }
        public string Phone { get; init; }
    }
}