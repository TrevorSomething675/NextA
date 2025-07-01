using Nexta.Application.DTO.Admin;

namespace Nexta.Application.Queries.Admin.GetDetailQuery
{
    public class GetAdminDetailQueryResponse(AdminDetailResponse detail)
    {
        public AdminDetailResponse Detail { get; init; } = detail;
    }
}