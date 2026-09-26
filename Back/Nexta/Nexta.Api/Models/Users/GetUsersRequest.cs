namespace Nexta.Web.Models.Users
{
    public class GetUsersRequest
    {
        public string? SearchTerm { get; set; } = string.Empty;
        public int? PageNumber { get; set; } = 1;
        public int? PageSize { get; set; } = 16;
    }
}