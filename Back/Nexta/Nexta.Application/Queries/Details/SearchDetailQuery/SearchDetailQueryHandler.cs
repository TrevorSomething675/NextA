using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Models.DataModels;
using Nexta.Domain.Models;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Queries.Details.SearchDetailQuery
{
	public class SearchDetailQueryHandler : IRequestHandler<SearchDetailQueryRequest, SearchDetailQueryResponse>
	{
		private readonly IMapper _mapper;
		private readonly IDetailRepository _detailRepository;

		public SearchDetailQueryHandler(IDetailRepository detailRepository, IMapper mapper) 
		{
			_detailRepository = detailRepository;
			_mapper = mapper;
		}

		public async Task<SearchDetailQueryResponse> Handle(SearchDetailQueryRequest request, CancellationToken ct = default)
		{
			var details = _mapper.Map<PagedData<Detail>>(await _detailRepository.SearchDetail(request.Filter, ct));

			return new SearchDetailQueryResponse(details);
		}
	}
}