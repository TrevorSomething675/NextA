using Nexta.Application.DTO.Products;
using Nexta.Domain.Specification;
using Nexta.Domain.Abstractions;
using Nexta.Domain.Base;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Queries.Products.GetProductsQuery
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, PagedData<ProductDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetProductsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PagedData<ProductDto>> Handle(GetProductsQuery query, CancellationToken ct)
        {
            var spec = new ProductSpecification(query.PageNumber, query.PageSize);
            var products = await _unitOfWork.Products.GetAsync(spec, ct);

            var response = _mapper.Map<PagedData<ProductDto>>(products);

            return response;
        }
    }
}