namespace Nexta.Domain.Filters.Users
{
    public class GetAdminUsersFilter : BaseFilter
    {
        public string SearchTerm { get; set; } = string.Empty;
    }
}