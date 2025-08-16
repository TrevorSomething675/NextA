namespace Nexta.Application.DTO.Response
{
    public class NewsResponse
    {
        public Guid Id { get; init; }
        public string? Header { get; init; }
        public string? Description { get; init; }

        public NewsImageResponse? Image { get; init; }
    }
}