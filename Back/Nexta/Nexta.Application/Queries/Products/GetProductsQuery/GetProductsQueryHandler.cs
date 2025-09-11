using Nexta.Domain.Abstractions.Repositories;
using Nexta.Application.DTO.Response;
using Nexta.Domain.Models.DataModels;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Queries.Products.GetProductsQuery
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, GetProductsQueryResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<GetProductsQueryResponse> Handle(GetProductsQuery query, CancellationToken ct)
        {
            var products = _mapper.Map<PagedData<ProductResponse>>(await _productRepository.GetAllAsync(query.Filter, ct));

            return new GetProductsQueryResponse(products);
        }
    }
}