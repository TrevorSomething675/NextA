namespace Nexta.Application.DTO
{
    public class ImageResponse
    {
		public string Name { get; init; } = null!;
		public string Bucket { get; set; } = null!;
		public string Base64String { get; init; } = null!;
	}
}