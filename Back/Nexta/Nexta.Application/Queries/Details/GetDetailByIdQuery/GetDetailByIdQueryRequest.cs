using MediatR;

namespace Nexta.Application.Queries.Details.GetDetailByIdQuery
{
    public class GetDetailByIdQueryRequest : IRequest<GetDetailByIdQueryResponse>
    {
        public Guid Id { get; set; }
    }
}