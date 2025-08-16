using Nexta.Application.Commands.Admin.UpdateOrderDetailCommand;
using Nexta.Domain.Models;
using AutoMapper;
using Nexta.Application.DTO.Request;
using Nexta.Application.DTO.Response;

namespace Nexta.Application.Profiles
{
    public class OrderDetailProfile : Profile
    {
        public OrderDetailProfile()
        {
            CreateMap<OrderDetail, OrderDetailResponse>();
            CreateMap<OrderDetailsRequest, OrderDetail>();
            CreateMap<UpdateOrderDetailCommandRequest, OrderDetail>();
        }
    }
}