namespace Nexta.Domain.Models
{
    public class UserDetail
    {
        public Guid UserId { get; set; }
        public Guid DetailId { get; set; }
		public int Count { get; set; }
	}
}