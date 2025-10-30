using Nexta.Domain.Models.Users;

namespace Nexta.Domain.Specification
{
    public class AdminUsersSpecification : BaseSpecification<User>
    {
        public AdminUsersSpecification(string searchTerm, int pageNumber, int pageSize)
        {
            SearchTerm = searchTerm;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
