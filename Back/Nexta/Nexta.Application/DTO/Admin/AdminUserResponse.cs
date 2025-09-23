using Nexta.Domain.Models;

namespace Nexta.Application.DTO.Admin
{
    public class AdminUserResponse
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }

        public string? Email { get; set; }
        public string? Phone { get; set; }

        public string Role { get; set; } = "User";

        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    }
}