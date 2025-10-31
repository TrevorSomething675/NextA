using Nexta.Application.DTO.Admin;
using Nexta.Domain.Base;
using MediatR;

namespace Nexta.Application.Queries.Admin.GetUsersQuery
{
    public class GetUsersQuery : IRequest<PagedData<AdminUserResponse>>
    {
        public string SearchTerm { get; set; } = string.Empty;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 16;
    }
}
