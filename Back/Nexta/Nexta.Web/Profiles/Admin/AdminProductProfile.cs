using Nexta.Application.Commands.Admin.CreateAdminProductCommand;
using Nexta.Application.Queries.Admin.GetProductsQuery;
using Nexta.Domain.Filters.Products;
using Nexta.Application.DTO.Admin;
using Nexta.Web.Areas.Models;
using Nexta.Domain.Models;
using AutoMapper;
using Nexta.Application.DTO.Request;
using Nexta.Application.Commands.Admin.UpdateProductCommand;

namespace Nexta.Web.Profiles.Admin
{
    public class AdminProductProfile : Profile
    {
        public AdminProductProfile()
        {
            CreateMap<Product, AdminProductResponse>()
                .ForMember(src => src.ImageId, opt => opt.MapFrom(x => x.ImageId))
                .ForMember(src => src.ImageName, opt => opt.MapFrom(x => x.Image != null ? x.Image.Name : ""))
                .ForMember(src => src.Base64String, opt => opt.MapFrom(x => x.Image != null ? x.Image.Base64String : ""));

            CreateMap<GetAdminProductsRequest, GetProductsFilter>()
                .ForMember(src => src.SearchTerm, opt => opt.MapFrom(x => x.SearchTerm ?? ""));

            CreateMap<GetAdminProductsRequest, GetAdminProductsQuery>()
                .ForMember(src => src.Filter, opt => opt.MapFrom(x => x));

            CreateMap<CreateAdminProductRequest, CreateAdminProductCommand>()
                .ForMember(src => src.Image, opt => opt.MapFrom(x =>
                    (!string.IsNullOrEmpty(x.ImageName) || !string.IsNullOrEmpty(x.ImageBase64String))
                        ? (new ImageRequest
                        {
                            Name = x.ImageName,
                            Base64String = x.ImageBase64String
                        }) : null));

            CreateMap<UpdateAdminProductRequest, UpdateAdminProductCommand>()
                .ForMember(dest => dest.Image, opt => opt.Condition(src =>
                    (src.ImageId != null) ||
                    !string.IsNullOrEmpty(src.ImageName) ||
                    !string.IsNullOrEmpty(src.ImageBase64String)))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src =>
                    new ProductImageRequest
                    {
                        Id = src.ImageId ?? null,
                        Name = src.ImageName,
                        Base64String = src.ImageBase64String
                    }));
        }
    }
}