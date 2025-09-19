namespace Nexta.Domain.Models
{
    public class Notification
    {
        public string Header { get; set; } = null!;
        public string Message { get; set; } = null!;
        public bool IsRead { get; set; }

        public DateTimeOffset CreatedDate { get; set; }
        public bool IsTemporary { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}