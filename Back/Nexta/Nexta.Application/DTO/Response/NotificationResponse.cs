namespace Nexta.Application.DTO.Response
{
    public class NotificationResponse
    {
        public Guid Id { get; set; }
        public string Header { get; set; } = null!;
        public string Message { get; set; } = null!;
        public bool IsRead { get; set; }

        public DateTimeOffset CreatedDate { get; set; }
        public bool IsTemporary { get; set; }

        public Guid UserId { get; set; }
        public UserResponse User { get; set; }
    }
}