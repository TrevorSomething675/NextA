using Nexta.Domain.Abstractions.Repositories;
using Nexta.Application.DTO.Admin;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Queries.Admin.GetProductQuery
{
	public class GetAdminProductQueryHandler : IRequestHandler<GetAdminProductQuery, GetAdminProductQueryResponse>
	{
		private readonly IProductRepository _productRepository;
		private readonly IMapper _mapper;

		public GetAdminProductQueryHandler(IProductRepository productRepository, IMapper mapper)
		{
			_productRepository = productRepository;
			_mapper = mapper;
		}

		public async Task<GetAdminProductQueryResponse> Handle(GetAdminProductQuery request, CancellationToken ct = default)
		{
			var product = _mapper.Map<AdminProductResponse>(await _productRepository.GetAsync(request.ProductId, ct));

			return new GetAdminProductQueryResponse(product);
		}
	}
}