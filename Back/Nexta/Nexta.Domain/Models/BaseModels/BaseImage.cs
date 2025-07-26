namespace Nexta.Domain.Models.BaseModels
{
    public class BaseImage
    {
		public Guid Id { get; set; }
		public string Name { get; set; } = null!;
		public string? Base64String { get; set; }
	}
}