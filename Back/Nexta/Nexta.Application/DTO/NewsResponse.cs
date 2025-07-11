namespace Nexta.Application.DTO
{
    public class NewsResponse
    {
        public string? Header { get; init; }
        public string? Description { get; init; }

        public ImageResponse? Image { get; init; }
    }
}