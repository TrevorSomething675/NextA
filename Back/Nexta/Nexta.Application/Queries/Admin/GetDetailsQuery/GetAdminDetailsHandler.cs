using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Models.DataModels;
using Nexta.Application.DTO.Admin;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Queries.Admin.GetDetailsQuery
{
	public class GetAdminDetailsHandler : IRequestHandler<GetAdminDetailsQueryRequest, GetAdminDetailsQueryResponse>
	{
		private readonly IDetailRepository _detailRepository;
		private readonly IMapper _mapper;

		public GetAdminDetailsHandler(IDetailRepository detailRepository, IMapper mapper)
		{
			_detailRepository = detailRepository;
			_mapper = mapper;
		}

		public async Task<GetAdminDetailsQueryResponse> Handle(GetAdminDetailsQueryRequest request, CancellationToken ct = default)
		{
			var details = _mapper.Map<PagedData<AdminDetailResponse>>(await _detailRepository.GetAllAsync(request.Filter, ct));

			return new GetAdminDetailsQueryResponse(details);
		}
	}
}