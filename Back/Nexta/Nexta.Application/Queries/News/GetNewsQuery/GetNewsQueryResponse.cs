using Nexta.Application.DTO;

namespace Nexta.Application.Queries.News.GetNewsQuery
{
    public class GetNewsQueryResponse(List<NewsResponse> news)
    {
        public List<NewsResponse> News { get; init; } = news;
    }
}