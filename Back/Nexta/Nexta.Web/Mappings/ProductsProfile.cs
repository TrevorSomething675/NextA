using Nexta.Application.Queries.Products.GetProductsQuery;
using Nexta.Application.Queries.Admin.GetProductsQuery;
using Nexta.Web.Models.Products;
using Nexta.Web.Areas.Models;
using AutoMapper;

namespace Nexta.Web.Mappings
{
    public class ProductsProfile : Profile
    {
        public ProductsProfile()
        {
            CreateMap<GetProductsRequest, GetProductsQuery>();

            CreateMap<GetAdminProductsRequest, GetAdminProductsQuery>();
        }
    }
}