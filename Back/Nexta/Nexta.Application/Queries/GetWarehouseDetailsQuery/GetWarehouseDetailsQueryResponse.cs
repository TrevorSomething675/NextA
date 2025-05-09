using Nexta.Domain.Models.DataModels;
using Nexta.Domain.Models;

namespace Nexta.Application.Queries.GetWarehouseDetailsQuery
{
    public class GetWarehouseDetailsQueryResponse(PagedData<Detail> details)
    {
        public PagedData<Detail> Details { get; set; } = details;
    }
}