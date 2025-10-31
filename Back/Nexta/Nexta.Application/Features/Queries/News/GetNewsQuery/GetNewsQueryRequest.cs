using Nexta.Application.Commands.News;
using MediatR;

namespace Nexta.Application.Queries.News.GetNewsQuery
{
    public class GetNewsQueryRequest : IRequest<List<NewsDto>> { }
}