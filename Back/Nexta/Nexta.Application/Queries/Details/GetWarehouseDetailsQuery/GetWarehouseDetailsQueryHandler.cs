using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Models.DataModels;
using AutoMapper;
using MediatR;
using Nexta.Application.DTO.Response;

namespace Nexta.Application.Queries.Details.GetWarehouseDetailsQuery
{
	public class GetWarehouseDetailsQueryHandler : IRequestHandler<GetWarehouseDetailsQueryRequest, GetWarehouseDetailsQueryResponse>
	{
		private readonly IDetailRepository _detailRepository;
		private readonly IMapper _mapper;

		public GetWarehouseDetailsQueryHandler(IDetailRepository detailRepository, IMapper mapper)
		{
			_detailRepository = detailRepository;
			_mapper = mapper;
		}

		public async Task<GetWarehouseDetailsQueryResponse> Handle(GetWarehouseDetailsQueryRequest request, CancellationToken ct)
		{
			var details = _mapper.Map<PagedData<DetailResponse>>(await _detailRepository.GetAllAsync(request.Filter, ct));

			return new GetWarehouseDetailsQueryResponse(details);
		}
	}
}