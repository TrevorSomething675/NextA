using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Models.DataModels;
using Nexta.Application.DTO.Admin;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Queries.Admin.GetProductsQuery
{
	public class GetAdminProductsQueryHandler : IRequestHandler<GetAdminProductsQuery, GetAdminProductsQueryResponse>
	{
		private readonly IProductRepository _productRepository;
		private readonly IMapper _mapper;

		public GetAdminProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
		{
			_productRepository = productRepository;
			_mapper = mapper;
		}

		public async Task<GetAdminProductsQueryResponse> Handle(GetAdminProductsQuery query, CancellationToken ct = default)
		{
			var details = _mapper.Map<PagedData<AdminProductResponse>>(await _productRepository.GetAllAsync(query.Filter, ct));

			return new GetAdminProductsQueryResponse(details);
		}
	}
}