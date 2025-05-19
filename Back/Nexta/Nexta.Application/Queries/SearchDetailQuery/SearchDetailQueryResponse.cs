using Nexta.Domain.Models.DataModels;
using Nexta.Domain.Models;

namespace Nexta.Application.Queries.SearchDetailQuery
{
    public class SearchDetailQueryResponse(PagedData<Detail> details)
    {
        public PagedData<Detail> Details { get; set; } = details;
    }
}