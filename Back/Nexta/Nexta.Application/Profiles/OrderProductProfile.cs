using Nexta.Application.DTO.Response;
using Nexta.Application.DTO.Request;
using Nexta.Domain.Models;
using AutoMapper;

namespace Nexta.Application.Profiles
{
    public class OrderProductProfile : Profile
    {
        public OrderProductProfile()
        {
            CreateMap<OrderProduct, OrderProductResponse>();
            CreateMap<OrderProductsRequest, OrderProduct>();
        }
    }
}