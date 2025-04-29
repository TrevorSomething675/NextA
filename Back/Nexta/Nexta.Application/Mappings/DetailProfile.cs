using Nexta.Domain.Entities;
using Nexta.Domain.Models;
using AutoMapper;

namespace Nexta.Application.Mappings
{
    public class DetailProfile : Profile
    {
        public DetailProfile()
        {
            CreateMap<Detail, DetailEntity>();
        }
	}
}