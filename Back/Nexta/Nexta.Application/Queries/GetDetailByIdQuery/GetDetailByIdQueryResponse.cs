using Nexta.Domain.Models;

namespace Nexta.Application.Queries.GetDetailByIdQuery
{
    public class GetDetailByIdQueryResponse(Detail detail)
    {
        public Detail Detail { get; set; } = detail;
    }
}