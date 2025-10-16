using Nexta.Domain.Base;

namespace Nexta.Domain.Models.User
{
    public class Notification : Entity
    {
        public Guid UserId { get; private set; }

        public string Header { get; private set; }
        public string Message { get; private set; }
        public bool IsReaded { get; private set; }
        
        public DateTimeOffset CreatedDate { get; private set; }

        public Notification(string header, string message, Guid userId)
        {
            Header = header;
            UserId = userId;
            IsReaded = false;
            Message = message;
            CreatedDate = DateTimeOffset.Now;
        }
    }
}