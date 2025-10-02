using Nexta.Domain.Abstractions.Repositories;
using Nexta.Application.DTO.Response;
using Nexta.Domain.Models;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Queries.Categories.GetCategoriesQuery
{
    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, GetCategoriesQueryResponse>
    {
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly IMapper _mapper;

        public GetCategoriesQueryHandler(ICategoriesRepository categoriesRepository, IMapper mapper)
        {
            _categoriesRepository = categoriesRepository;
            _mapper = mapper;
        }

        public async Task<GetCategoriesQueryResponse> Handle(GetCategoriesQuery query, CancellationToken ct)
        {
            var dbCategories = await _categoriesRepository.GetAsync(ct);
            var categories = _mapper.Map<List<ProductCategory>>(dbCategories);

            var categoriesResponse = _mapper.Map<List<ProductCategoryResponse>>(categories);

            return new GetCategoriesQueryResponse(categoriesResponse);
        }
    }
}
