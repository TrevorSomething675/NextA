using Nexta.Application.Queries.Admin.GetProductsQuery;
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

            CreateMap<GetAdminProductsRequest, GetAdminProductsQuery>();
        }
    }
}