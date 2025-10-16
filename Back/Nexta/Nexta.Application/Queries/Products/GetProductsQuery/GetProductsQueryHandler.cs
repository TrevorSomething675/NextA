using Nexta.Domain.Abstractions.Repositories;
using Nexta.Application.DTO.Response;
using AutoMapper;
using MediatR;
using Nexta.Domain.Base;

namespace Nexta.Application.Queries.Products.GetProductsQuery
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, GetProductsQueryResponse>
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IMapper _mapper;

        public GetProductsQueryHandler(IProductsRepository productsRepository, IMapper mapper)
        {
            _productsRepository = productsRepository;
            _mapper = mapper;
        }

        public async Task<GetProductsQueryResponse> Handle(GetProductsQuery query, CancellationToken ct)
        {
            var products = _mapper.Map<PagedData<ProductResponse>>(await _productsRepository.GetAllAsync(query.Filter, ct));

            return new GetProductsQueryResponse(products);
        }
    }
}