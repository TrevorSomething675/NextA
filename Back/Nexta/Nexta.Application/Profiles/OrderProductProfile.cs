using Nexta.Application.DTO.Request;
using Nexta.Domain.Models.Order;
using AutoMapper;

namespace Nexta.Application.Profiles
{
    public class OrderProductProfile : Profile
    {
        public OrderProductProfile()
        {
            CreateMap<OrderProductsRequest, OrderItem>();
        }
    }
}