using Nexta.Application.DTO.Product;
using Nexta.Domain.Specification;
using Nexta.Domain.Abstractions;
using Nexta.Domain.Base;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Queries.Products.GetProductsQuery
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, GetProductsQueryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetProductsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetProductsQueryResponse> Handle(GetProductsQuery query, CancellationToken ct)
        {
            var spec = new ProductSpecification(query.Filter.PageNumber, query.Filter.PageSize);
            var products = await _unitOfWork.Products.GetAllAsync(spec, ct);

            var response = _mapper.Map<PagedData<ProductDto>>(products);

            return new GetProductsQueryResponse(response);
        }
    }
}