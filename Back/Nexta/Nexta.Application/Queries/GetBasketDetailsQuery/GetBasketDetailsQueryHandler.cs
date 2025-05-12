using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Models.DataModels;
using Nexta.Domain.Models;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Queries.GetUserBasketDetails
{
	public class GetBasketDetailsQueryHandler : IRequestHandler<GetBasketDetailsQueryRequest, Result<GetBasketDetailsQueryResponse>>
	{
		private readonly IDetailRepository _detailRepository;
		private readonly IMapper _mapper;
		public GetBasketDetailsQueryHandler(IDetailRepository detailRepository, IMapper mapper)
		{
			_detailRepository = detailRepository;
			_mapper = mapper;
		}

		public async Task<Result<GetBasketDetailsQueryResponse>> Handle(GetBasketDetailsQueryRequest request, CancellationToken ct)
		{
			try
			{
				var pagedBasketDetails = _mapper.Map<List<Detail>>(await _detailRepository.GetBasketDetailsAsync(request.Filter, ct));
				var jija = pagedBasketDetails.FirstOrDefault().DeliveryDate.ToString();
				var jija2 = pagedBasketDetails.FirstOrDefault().DeliveryDate;

				return new Result<GetBasketDetailsQueryResponse>(new GetBasketDetailsQueryResponse(pagedBasketDetails)).Success();
			}
			catch(Exception ex)
			{
				return new Result<GetBasketDetailsQueryResponse>().BadRequest(ex.Message);
			}
		}
	}
}