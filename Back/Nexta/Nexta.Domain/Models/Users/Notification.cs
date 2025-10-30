using Nexta.Domain.Base;

namespace Nexta.Domain.Models.Users
{
    public class Notification : Entity
    {
        public Guid UserId { get; private set; }

        public string Header { get; private set; }
        public string Message { get; private set; }
        public bool IsRead { get; private set; }
        
        public DateTime CreatedDate { get; private set; }

        public Notification(string header, string message, Guid userId)
        {
            Header = header;
            UserId = userId;
            IsRead = false;
            Message = message;
            CreatedDate = DateTime.UtcNow;
        }
    }
}