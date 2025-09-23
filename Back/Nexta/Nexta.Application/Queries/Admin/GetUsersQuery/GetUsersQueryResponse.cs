using Nexta.Domain.Models.DataModels;
using Nexta.Application.DTO.Admin;
using Nexta.Application.Common;

namespace Nexta.Application.Queries.Admin.GetUsersQuery
{
    public class GetUsersQueryResponse : BasePagedResponse<AdminUserResponse>
    {
        public GetUsersQueryResponse(PagedData<AdminUserResponse> data) : base(data) { }
    }
}