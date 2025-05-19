using Nexta.Domain.Models.DataModels;
using Nexta.Domain.Filters;
using MediatR;

namespace Nexta.Application.Queries.SearchDetailQuery
{
    public class SearchDetailQueryRequest : IRequest<Result<SearchDetailQueryResponse>>
    {
        public SearchDetailFilter Filter { get; set; } = null!;
    }
}