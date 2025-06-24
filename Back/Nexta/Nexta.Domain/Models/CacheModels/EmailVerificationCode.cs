namespace Nexta.Domain.Models.CacheModels
{
    public class EmailVerificationCode
    {
        public string Email { get; set; }
        public string Code { get; set; }
        public DateTime ExpiryTime { get; set; }
        public bool IsVerified { get; set; }
    }
}