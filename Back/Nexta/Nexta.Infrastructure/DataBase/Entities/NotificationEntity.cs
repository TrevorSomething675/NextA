namespace Nexta.Infrastructure.DataBase.Entities
{
    public class NotificationEntity : BaseEntity
    {
        public string Header { get; set; } = null!;
        public string Message { get; set; } = null!;
        public bool IsRead { get; set; } = false;

        public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.UtcNow;
        public bool IsTemporary { get; set; } = true;

        public Guid UserId { get; set; }
        public UserEntity User { get; set; } = null!;
    }
}