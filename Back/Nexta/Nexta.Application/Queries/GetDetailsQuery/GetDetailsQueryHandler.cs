using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Models.DataModels;
using Nexta.Domain.Models;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Queries.GetDetailsQuery
{
	public class GetDetailsQueryHandler : IRequestHandler<GetDetailsQueryRequest, Result<GetDetailsQueryResponse>>
    {
        private readonly IDetailRepository _detailRepository;
        private readonly IMapper _mapper;

		public GetDetailsQueryHandler(IDetailRepository detailRepository, IMapper mapper) 
        {
            _detailRepository = detailRepository;
            _mapper = mapper;
        }
		public async Task<Result<GetDetailsQueryResponse>> Handle(GetDetailsQueryRequest request, CancellationToken ct)
		{
            try
            {
                var pagedDetails = _mapper.Map<PagedData<Detail>>(await _detailRepository.GetAllAsync(request.Filter, ct));

                var response = new GetDetailsQueryResponse(pagedDetails);

                return new Result<GetDetailsQueryResponse>(response).Success();

            }
            catch (Exception ex)
            {
                return new Result<GetDetailsQueryResponse>().Invalid(ex.Message);
            }
		}
	}
}