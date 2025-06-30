using Nexta.Application.DTO;

namespace Nexta.Application.Queries.Details.GetDetailByIdQuery
{
    public class GetDetailByIdQueryResponse(DetailResponse detail)
    {
		public DetailResponse User { get; init; } = detail;
	}
}