using MediatR;

namespace Nexta.Application.Queries.GetDetailByIdQuery
{
    public class GetDetailByIdQueryRequest : IRequest<GetDetailByIdQueryResponse>
    {
        public Guid Id { get; set; }
    }
}