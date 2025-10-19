using Nexta.Application.DTO.Product;
using Nexta.Domain.Specification;
using Nexta.Domain.Abstractions;
using Nexta.Domain.Base;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Queries.Admin.GetProductsQuery
{
	public class GetAdminProductsQueryHandler : IRequestHandler<GetAdminProductsQuery, PagedData<AdminProductDto>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public GetAdminProductsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<PagedData<AdminProductDto>> Handle(GetAdminProductsQuery query, CancellationToken ct = default)
		{
			var spec = new ProductSpecification(query.Filter.PageNumber, query.Filter.PageSize);

			var products = await _unitOfWork.Products.GetAllAsync(spec, ct);

			var response = _mapper.Map<PagedData<AdminProductDto>>(products);

			return response;
		}
	}
}