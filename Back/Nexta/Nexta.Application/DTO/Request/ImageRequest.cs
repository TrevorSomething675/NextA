namespace Nexta.Application.DTO.Request
{
    public class ImageRequest
    {
		public Guid? Id { get; set; }
		public string? Name { get; init; }
		public string? Base64String { get; init; }
	}
}