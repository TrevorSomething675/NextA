using Nexta.Domain.Abstractions.Repositories;
using Nexta.Application.DTO.Admin;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Queries.Admin.GetProductQuery
{
	public class GetAdminProductQueryHandler : IRequestHandler<GetAdminProductQuery, GetAdminProductQueryResponse>
	{
		private readonly IProductsRepositoryL _productsRepository;
		private readonly IMapper _mapper;

		public GetAdminProductQueryHandler(IProductsRepositoryL productsRepository, IMapper mapper)
		{
			_productsRepository = productsRepository;
			_mapper = mapper;
		}

		public async Task<GetAdminProductQueryResponse> Handle(GetAdminProductQuery request, CancellationToken ct = default)
		{
			var product = _mapper.Map<AdminProductResponse>(await _productsRepository.GetAsync(request.ProductId, ct));

			return new GetAdminProductQueryResponse(product);
		}
	}
}