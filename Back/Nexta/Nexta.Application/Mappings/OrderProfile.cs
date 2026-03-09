using Nexta.Application.DTO.Orders;
using Nexta.Domain.Models.Orders;
using Nexta.Domain.Base;
using AutoMapper;

namespace Nexta.Application.Mappings
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderItem, OrderItemDto>().ReverseMap();
            CreateMap<Order, OrderDto>()
                .ForMember(src => src.Products, opt => opt.MapFrom(x => x.Products));
            CreateMap<PagedData<Order>, PagedData<OrderDto>>().ReverseMap();
        }
    }
}
