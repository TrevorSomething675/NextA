using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Models.DataModels;
using Nexta.Application.DTO;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Queries.Details.GetDetailsQuery
{
	public class GetDetailsQueryHandler : IRequestHandler<GetDetailsQueryRequest, GetDetailsQueryResponse>
    {
        private readonly IDetailRepository _detailRepository;
        private readonly IMapper _mapper;

		public GetDetailsQueryHandler(IDetailRepository detailRepository, IMapper mapper) 
        {
            _detailRepository = detailRepository;
            _mapper = mapper;
        }

		public async Task<GetDetailsQueryResponse> Handle(GetDetailsQueryRequest request, CancellationToken ct)
		{
            var details = _mapper.Map<PagedData<DetailResponse>>(await _detailRepository.GetAllAsync(request.Filter, ct));

            return new GetDetailsQueryResponse(details);
		}
	}
}