using Nexta.Application.Queries.Products.GetProductsQuery;
using Nexta.Application.Queries.Admin.GetProductsQuery;
using Nexta.Domain.Filters.Products;
using Nexta.Web.Models.Products;
using Nexta.Web.Areas.Models;
using AutoMapper;

namespace Nexta.Web.Profiles
{
    public class ProductsProfile : Profile
    {
        public ProductsProfile()
        {
            CreateMap<GetProductsRequest, GetProductsFilter>()
                .ForMember(src => src.SearchTerm, opt => opt.MapFrom(x => x.SearchTerm ?? ""));
            CreateMap<GetProductsRequest, GetProductsQuery>()
                .ForMember(src => src.Filter, opt => opt.MapFrom(x => x));

            CreateMap<GetAdminProductsRequest, GetProductsFilter>();
            CreateMap<GetAdminProductsQuery, GetAdminProductsQuery>()
                .ForMember(src => src.Filter, opt => opt.MapFrom(x => x));
        }
    }
}