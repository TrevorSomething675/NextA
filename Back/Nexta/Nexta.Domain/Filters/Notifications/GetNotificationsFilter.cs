namespace Nexta.Domain.Filters.Notifications
{
    public class GetNotificationsFilter : BaseFilter
    {
        public string SearchTerm { get; set; } = string.Empty;
        public Guid UserId { get; set; }
    }
}