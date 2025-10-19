using Nexta.Application.DTO.Product;
using Nexta.Domain.Abstractions;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Queries.Admin.GetProductQuery
{
	public class GetAdminProductQueryHandler : IRequestHandler<GetAdminProductQuery, AdminProductDto>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public GetAdminProductQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<AdminProductDto> Handle(GetAdminProductQuery request, CancellationToken ct = default)
		{
			var product = await _unitOfWork.Products.GetAsync(request.ProductId, ct);
			var response = _mapper.Map<AdminProductDto>(product);

			return response;
        }
	}
}