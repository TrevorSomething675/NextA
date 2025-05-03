using Nexta.Domain.Entities;
using Nexta.Domain.Models;
using AutoMapper;
using Nexta.Domain.Models.DataModels;

namespace Nexta.Application.Mappings
{
    public class DetailProfile : Profile
    {
        public DetailProfile()
        {
            CreateMap<Detail, DetailEntity>().ReverseMap();
            CreateMap<PagedData<DetailEntity>, PagedData<Detail>>();
        }
	}
}