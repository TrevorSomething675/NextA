using Nexta.Domain.Abstractions.Repositories;
using Nexta.Application.DTO.Response;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Queries.Products.GetProductByIdQuery
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, GetProductByIdQueryResponse>
    {
        private readonly IMapper _mapper;
        private readonly IProductsRepositoryL _productsRepository;

        public GetProductByIdQueryHandler(IProductsRepositoryL productsRepository, IMapper mapper)
        {
            _productsRepository = productsRepository;
            _mapper = mapper;
        }

        public async Task<GetProductByIdQueryResponse> Handle(GetProductByIdQuery query, CancellationToken ct = default)
        {
            var product = _mapper.Map<ProductResponse>(await _productsRepository.GetAsync(query.Id, ct));

            return new GetProductByIdQueryResponse(product);
        }
    }
}