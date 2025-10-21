using Nexta.Application.DTO.Admin;
using Nexta.Domain.Filters.Users;
using Nexta.Domain.Base;
using MediatR;

namespace Nexta.Application.Queries.Admin.GetUsersQuery
{
    public class GetUsersQuery : IRequest<PagedData<AdminUserResponse>>
    {
        public GetAdminUsersFilter Filter { get; init; }
    }
}
