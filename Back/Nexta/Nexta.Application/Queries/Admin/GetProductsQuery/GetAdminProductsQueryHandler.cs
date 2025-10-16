using Nexta.Domain.Abstractions.Repositories;
using Nexta.Application.DTO.Admin;
using AutoMapper;
using MediatR;
using Nexta.Domain.Base;

namespace Nexta.Application.Queries.Admin.GetProductsQuery
{
	public class GetAdminProductsQueryHandler : IRequestHandler<GetAdminProductsQuery, GetAdminProductsQueryResponse>
	{
		private readonly IProductsRepository _productsRepository;
		private readonly IMapper _mapper;

		public GetAdminProductsQueryHandler(IProductsRepository productsRepository, IMapper mapper)
		{
			_productsRepository = productsRepository;
			_mapper = mapper;
		}

		public async Task<GetAdminProductsQueryResponse> Handle(GetAdminProductsQuery query, CancellationToken ct = default)
		{
			var products = _mapper.Map<PagedData<AdminProductResponse>>(await _productsRepository.GetAllAsync(query.Filter, ct));

			return new GetAdminProductsQueryResponse(products);
		}
	}
}