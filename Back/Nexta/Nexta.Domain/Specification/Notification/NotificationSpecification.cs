using Nexta.Domain.Models.Users;

namespace Nexta.Domain.Specification
{
    public class NotificationSpecification : BaseSpecification<Notification>
    {
        public NotificationSpecification(string searchTerm, Guid userId, int pageNumber, int pageSize)
        {
            SearchTerm = searchTerm;
            Creteria = n => n.UserId == userId;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
