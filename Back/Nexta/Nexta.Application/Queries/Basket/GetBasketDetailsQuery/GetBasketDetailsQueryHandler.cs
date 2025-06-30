using Nexta.Domain.Abstractions.Repositories;
using Nexta.Application.DTO;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Queries.Basket.GetBasketDetailsQuery
{
	public class GetBasketDetailsQueryHandler : IRequestHandler<GetBasketDetailsQueryRequest, GetBasketDetailsQueryResponse>
	{
		private readonly IDetailRepository _detailRepository;
		private readonly IMapper _mapper;
		public GetBasketDetailsQueryHandler(IDetailRepository detailRepository, IMapper mapper)
		{
			_detailRepository = detailRepository;
			_mapper = mapper;
		}

		public async Task<GetBasketDetailsQueryResponse> Handle(GetBasketDetailsQueryRequest request, CancellationToken ct = default)
		{
			var details = _mapper.Map<List<DetailResponse>>(await _detailRepository.GetBasketDetailsAsync(request.Filter, ct));

			return new GetBasketDetailsQueryResponse(details);
		}
	}
}