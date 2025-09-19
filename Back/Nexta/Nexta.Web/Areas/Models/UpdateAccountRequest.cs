namespace Nexta.Web.Areas.Models
{
    public class UpdateAccountRequest
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }

        public string? Phone { get; set; }
    }
}