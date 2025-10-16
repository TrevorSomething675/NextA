using AutoMapper;
using Nexta.Application.DTO.Request;
using Nexta.Application.DTO.Response;
using Nexta.Domain.Models.Order;

namespace Nexta.Application.Profiles
{
    public class OrderProductProfile : Profile
    {
        public OrderProductProfile()
        {
            CreateMap<OrderProductsRequest, OrderProduct>();
            CreateMap<OrderProduct, OrderProductResponse>()
                .ForMember(src => src.Id, opt => opt.MapFrom(x => x.ProductId))
                .ForMember(src => src.Name, opt => opt.MapFrom(x => x.Product!.Name))
                .ForMember(src => src.Article, opt => opt.MapFrom(x => x.Product!.Article))
                .ForMember(src => src.Description, opt => opt.MapFrom(x => x.Product!.Description))
                .ForMember(src => src.NewPrice, opt => opt.MapFrom(x => x.Product!.NewPrice))
                .ForMember(src => src.OldPrice, opt => opt.MapFrom(x => x.Product!.OldPrice))
                .ForMember(src => src.Count, opt => opt.MapFrom(x => x.Count));
        }
    }
}