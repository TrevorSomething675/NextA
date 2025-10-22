namespace Nexta.Application.DTO.User
{
    public class NotificationDto
    {
        public Guid UserId { get; init; }

        public string Header { get; init; }
        public string Message { get; init; }
        public bool IsRead { get; init; }

        public DateTimeOffset CreatedDate { get; init; }
    }
}