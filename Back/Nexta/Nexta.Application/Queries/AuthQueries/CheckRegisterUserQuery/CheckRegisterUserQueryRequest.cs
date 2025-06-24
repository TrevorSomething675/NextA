using MediatR;

namespace Nexta.Application.Queries.AuthQueries.CheckRegisterUserQuery
{
    public class CheckRegisterUserQueryRequest : IRequest<CheckRegisterUserQueryResponse>
    {
        public string Email { get; set; }
    }
}