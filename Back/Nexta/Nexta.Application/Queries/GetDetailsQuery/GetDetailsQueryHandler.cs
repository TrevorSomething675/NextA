using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Models.DataModels;
using Nexta.Domain.Models;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Queries.GetDetailsQuery
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
            var pagedDetails = _mapper.Map<PagedData<Detail>>(await _detailRepository.GetAllAsync(request.Filter, ct));

            return new GetDetailsQueryResponse(pagedDetails);
		}
	}
}