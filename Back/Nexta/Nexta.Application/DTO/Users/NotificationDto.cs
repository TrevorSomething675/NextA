namespace Nexta.Application.DTO.Users
{
    public class NotificationDto
    {
        public Guid UserId { get; init; }

        public string Header { get; init; }
        public string Message { get; init; }
        public bool IsRead { get; init; }

        public DateTime CreatedDate { get; init; }
    }
}