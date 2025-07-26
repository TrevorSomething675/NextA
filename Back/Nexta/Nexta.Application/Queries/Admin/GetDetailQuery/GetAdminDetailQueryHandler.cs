using Nexta.Domain.Abstractions.Repositories;
using Nexta.Application.DTO.Admin;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Queries.Admin.GetDetailQuery
{
	public class GetAdminDetailQueryHandler : IRequestHandler<GetAdminDetailQueryRequest, GetAdminDetailQueryResponse>
	{
		private readonly IDetailRepository _detailRepository;
		private readonly IMapper _mapper;

		public GetAdminDetailQueryHandler(IDetailRepository detailRepository, IMapper mapper)
		{
			_detailRepository = detailRepository;
			_mapper = mapper;
		}

		public async Task<GetAdminDetailQueryResponse> Handle(GetAdminDetailQueryRequest request, CancellationToken ct = default)
		{
			var detail = _mapper.Map<AdminDetailResponse>(await _detailRepository.GetAsync(request.DetailId, ct));

			return new GetAdminDetailQueryResponse(detail);
		}
	}
}