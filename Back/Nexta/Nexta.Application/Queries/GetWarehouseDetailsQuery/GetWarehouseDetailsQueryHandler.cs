using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Models.DataModels;
using Nexta.Domain.Models;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Queries.GetWarehouseDetailsQuery
{
	public class GetWarehouseDetailsQueryHandler : IRequestHandler<GetWarehouseDetailsQueryRequest, Result<GetWarehouseDetailsQueryResponse>>
	{
		private readonly IDetailRepository _detailRepository;
		private readonly IMapper _mapper;

		public GetWarehouseDetailsQueryHandler(IDetailRepository detailRepository, IMapper mapper)
		{
			_detailRepository = detailRepository;
			_mapper = mapper;
		}

		public async Task<Result<GetWarehouseDetailsQueryResponse>> Handle(GetWarehouseDetailsQueryRequest request, CancellationToken ct)
		{
			try
			{
				var pagedDetails = _mapper.Map<PagedData<Detail>>(await _detailRepository.GetAllAsync(request.Filter, ct));

				return new Result<GetWarehouseDetailsQueryResponse>(new GetWarehouseDetailsQueryResponse(pagedDetails));
			}
			catch(Exception ex)
			{
				return new Result<GetWarehouseDetailsQueryResponse>().BadRequest(ex.Message);
			}
		}
	}
}