using Nexta.Application.DTO.Response;

namespace Nexta.Application.Queries.Details.GetDetailByIdQuery
{
    public class GetDetailByIdQueryResponse(DetailResponse detail)
    {
		public DetailResponse Detail { get; init; } = detail;
	}
}