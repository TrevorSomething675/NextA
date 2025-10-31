using Nexta.Application.DTO.Products;
using AutoMapper;
using MediatR;
using Nexta.Application.Abstractions;

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
			var product = await _unitOfWork.Products.GetByIdAsync(request.ProductId, ct);
			var response = _mapper.Map<AdminProductDto>(product);

			return response;
        }
	}
}