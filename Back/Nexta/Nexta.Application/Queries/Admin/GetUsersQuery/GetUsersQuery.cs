using Nexta.Domain.Filters.Users;
using MediatR;

namespace Nexta.Application.Queries.Admin.GetUsersQuery
{
    public class GetUsersQuery : IRequest<GetUsersQueryResponse>
    {
        public GetAdminUsersFilter Filter { get; init; }
    }
}
