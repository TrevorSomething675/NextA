using Nexta.Domain.Models;

namespace Nexta.Application.Queries.GetNewsQuery
{
    public class GetNewsQueryResponse(List<Image> images)
    {
        public List<Image> Images { get; set; } = images;
    }
}