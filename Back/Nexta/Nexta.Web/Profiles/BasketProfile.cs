using Nexta.Domain.Filters.Products;
using Nexta.Web.Models.Basket;
using AutoMapper;
using Nexta.Application.Queries.Basket.GetBasketProductsQuery;

namespace Nexta.Web.Profiles
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<GetBasketProductsRequest, GetBasketProductsFilter>();

            CreateMap<GetBasketProductsRequest, GetBasketProductsQuery>()
                .ForMember(src => src.Filter, opt => opt.MapFrom(x => x));
        }
    }
}
