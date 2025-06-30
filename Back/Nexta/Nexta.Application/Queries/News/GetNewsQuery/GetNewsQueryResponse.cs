using Nexta.Application.DTO;

namespace Nexta.Application.Queries.News.GetNewsQuery
{
    public class GetNewsQueryResponse(List<ImageResponse> images)
    {
        public List<ImageResponse> Images { get; init; } = images;
    }
}