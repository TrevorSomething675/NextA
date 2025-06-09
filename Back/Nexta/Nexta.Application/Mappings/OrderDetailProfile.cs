using Nexta.Domain.Models.DataModels;
using Nexta.Domain.Entities;
using Nexta.Domain.Models;
using AutoMapper;

namespace Nexta.Application.Mappings
{
    public class OrderDetailProfile : Profile
	{
		public OrderDetailProfile()
		{
			CreateMap<OrderDetail, OrderDetailEntity>().ReverseMap();

			CreateMap<PagedData<OrderDetail>, PagedData<OrderDetailEntity>>().ReverseMap();
		}
	}
}