namespace Nexta.Domain.Models
{
    public class Image
    {
		public Guid Id { get; set; }
		public string Name { get; set; } = null!;
		public string Bucket { get; set; } = null!;
		public string? Base64String { get; set; }
	}
}