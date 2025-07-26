using Nexta.Domain.Abstractions.Repositories;
using Nexta.Application.DTO;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Queries.News.GetNewsQuery
{
	public class GetNewsQueryHandler : IRequestHandler<GetNewsQueryRequest, GetNewsQueryResponse>
	{
		private readonly INewsRepository _newsRepository;
		private readonly IMapper _mapper;

		public GetNewsQueryHandler(INewsRepository newsRepository, IMapper mapper)
		{
			_newsRepository = newsRepository;
			_mapper = mapper;
		}

		public async Task<GetNewsQueryResponse> Handle(GetNewsQueryRequest request, CancellationToken ct = default)
		{
			var news = await _newsRepository.GetAllAsync(ct);

			var result = _mapper.Map<List<NewsResponse>>(news);

			return new GetNewsQueryResponse(result);
		}
	}
}