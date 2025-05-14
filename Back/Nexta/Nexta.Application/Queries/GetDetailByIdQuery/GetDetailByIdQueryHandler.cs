using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Models.DataModels;
using Nexta.Domain.Models;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Queries.GetDetailByIdQuery
{
	public class GetDetailByIdQueryHandler : IRequestHandler<GetDetailByIdQueryRequest, Result<GetDetailByIdQueryResponse>>
	{
		private readonly IMapper _mapper;
		private readonly IDetailRepository _detailRepository;
		public GetDetailByIdQueryHandler(IDetailRepository detailRepository, IMapper mapper)
		{
			_detailRepository = detailRepository;
			_mapper = mapper;
		}

		public async Task<Result<GetDetailByIdQueryResponse>> Handle(GetDetailByIdQueryRequest request, CancellationToken ct)
		{
			try
			{
				var detail = _mapper.Map<Detail>(await _detailRepository.GetAsync(request.Id, ct));

				return new Result<GetDetailByIdQueryResponse>(new GetDetailByIdQueryResponse(detail));
			}
			catch (Exception ex)
			{
				return new Result<GetDetailByIdQueryResponse>().BadRequest(ex.Message);
			}
		}
	}
}