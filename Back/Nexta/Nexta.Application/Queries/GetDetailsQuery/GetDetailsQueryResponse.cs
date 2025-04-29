using Nexta.Domain.Models.DataModels;
using Nexta.Domain.Models;

namespace Nexta.Application.Queries.GetDetailsQuery
{
    public class GetDetailsQueryResponse(PagedData<Detail> pagedDetails)
    {
        public PagedData<Detail> PagedDetails { get; set; } = pagedDetails;
    }
}