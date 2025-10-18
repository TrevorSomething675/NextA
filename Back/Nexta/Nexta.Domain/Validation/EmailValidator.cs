using System.Net.Mail;

namespace Nexta.Domain.Validation
{
    public static class EmailValidator
    {
        public static bool IsValid(string? email)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}