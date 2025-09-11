using AutoMapper;
using Nexta.Application.Commands.Admin.CreateAdminProductCommand;
using Nexta.Application.Commands.Admin.UpdateAdminProductCommand;
using Nexta.Application.DTO.Admin;
using Nexta.Application.DTO.Request;
using Nexta.Application.DTO.Response;
using Nexta.Application.Queries.Admin.GetProductsQuery;
using Nexta.Domain.Filters.Products;
using Nexta.Domain.Models;
using Nexta.Domain.Models.Images;
using Nexta.Web.Areas.Models;

namespace Nexta.Web.Profiles.Admin
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

            CreateMap<ProductImage, ProductImageResponse>();

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