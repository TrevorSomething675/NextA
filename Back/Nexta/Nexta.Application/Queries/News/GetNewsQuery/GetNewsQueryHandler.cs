using Nexta.Domain.Abstractions.Repositories;
using Nexta.Application.Commands.News;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Queries.News.GetNewsQuery
{
	public class GetNewsQueryHandler : IRequestHandler<GetNewsQueryRequest, List<NewsDto>>
	{
		private readonly INewsRepository _newsRepository;
		private readonly IMapper _mapper;

		public GetNewsQueryHandler(INewsRepository newsRepository, IMapper mapper)
		{
			_newsRepository = newsRepository;
			_mapper = mapper;
		}

		public async Task<List<NewsDto>> Handle(GetNewsQueryRequest query, CancellationToken ct = default)
		{
			var news = await _newsRepository.GetAsync(ct);

			var response = _mapper.Map<List<NewsDto>>(news);

			return response;
        }
	}
}