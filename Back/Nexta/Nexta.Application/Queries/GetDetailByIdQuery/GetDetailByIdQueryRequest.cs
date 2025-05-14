using Nexta.Domain.Models.DataModels;
using MediatR;

namespace Nexta.Application.Queries.GetDetailByIdQuery
{
    public class GetDetailByIdQueryRequest : IRequest<Result<GetDetailByIdQueryResponse>>
    {
        public Guid Id { get; set; }
    }
}