﻿using Nexta.Domain.Abstractions.Repositories;
using Nexta.Application.DTO;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Queries.Details.GetDetailByIdQuery
{
	public class GetDetailByIdQueryHandler : IRequestHandler<GetDetailByIdQueryRequest, GetDetailByIdQueryResponse>
	{
		private readonly IMapper _mapper;
		private readonly IDetailRepository _detailRepository;
		public GetDetailByIdQueryHandler(IDetailRepository detailRepository, IMapper mapper)
		{
			_detailRepository = detailRepository;
			_mapper = mapper;
		}

		public async Task<GetDetailByIdQueryResponse> Handle(GetDetailByIdQueryRequest request, CancellationToken ct)
		{
			var detail = _mapper.Map<DetailResponse>(await _detailRepository.GetAsync(request.Id, ct));

			return new GetDetailByIdQueryResponse(detail);
		}
	}
}