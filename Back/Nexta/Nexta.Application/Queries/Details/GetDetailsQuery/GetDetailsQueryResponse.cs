using Nexta.Domain.Models.DataModels;
using Nexta.Domain.Models;

namespace Nexta.Application.Queries.Details.GetDetailsQuery
{
    public class GetDetailsQueryResponse(PagedData<Detail> details)
    {
        public PagedData<Detail> Details { get; set; } = details;
    }
}