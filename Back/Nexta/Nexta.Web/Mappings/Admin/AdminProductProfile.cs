using Nexta.Application.Queries.Admin.GetProductsQuery;
using Nexta.Domain.Filters.Products;
using Nexta.Domain.Models.Product;
using Nexta.Application.DTO.Admin;
using Nexta.Web.Areas.Models;
using AutoMapper;

namespace Nexta.Web.Mappings.Admin
{
    public class AdminProductProfile : Profile
    {
        public AdminProductProfile()
        {
            CreateMap<Product, AdminProductResponse>();
            /*
            .ForMember(src => src.Image.Id, opt => opt.MapFrom(x => x.ImageId))
            .ForMember(src => src.Image.Name, opt => opt.MapFrom(x => x.Image != null ? x.Image.Name : ""))
            .ForMember(src => src.Image.Base64String, opt => opt.MapFrom(x => x.Image != null ? x.Image.Base64String : ""));
            */

            CreateMap<GetAdminProductsRequest, GetProductsFilter>()
                .ForMember(src => src.SearchTerm, opt => opt.MapFrom(x => x.SearchTerm ?? ""));

            CreateMap<GetAdminProductsRequest, GetAdminProductsQuery>()
                .ForMember(src => src.Filter, opt => opt.MapFrom(x => x));
        }
    }
}