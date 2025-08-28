using Nexta.Application.Commands.Admin.CreateAdminProductCommand;
using Nexta.Application.Commands.Admin.UpdateDetailCommand;
using Nexta.Application.DTO.Response;
using Nexta.Domain.Models.DataModels;
using Nexta.Application.DTO.Request;
using Nexta.Application.DTO.Admin;
using Nexta.Domain.Models.Images;
using Nexta.Domain.Models;
using Nexta.Domain.Enums;
using AutoMapper;

namespace Nexta.Application.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile() 
        {
            CreateMap<Product, ProductResponse>();
            CreateMap<Product, AdminProductResponse>();
            CreateMap<PagedData<Product>, PagedData<AdminProductResponse>>();
            CreateMap<PagedData<Product>, PagedData<ProductResponse>>();
            CreateMap<UpdateAdminProductCommand, Product>()
                .ForMember(src => src.Status, opt => opt.MapFrom(x => (ProductStatus)x.Status));
            CreateMap<ProductRequest, Product>();
            CreateMap<ProductImageRequest, ProductImage>();
            CreateMap<ProductImage, ProductImageResponse>();
            CreateMap<ProductImage, AdminProductImageResponse>();
            CreateMap<CreateAdminProductCommand, Product>();
        }
    }
}