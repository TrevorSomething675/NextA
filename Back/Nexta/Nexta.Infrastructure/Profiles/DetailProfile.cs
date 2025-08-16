using Nexta.Infrastructure.DataBase.Entities;
using Nexta.Domain.Models.DataModels;
using Nexta.Domain.Models.Images;
using Nexta.Domain.Models;
using AutoMapper;

namespace Nexta.Infrastructure.Profiles
{
    public class DetailProfile : Profile
    {
        public DetailProfile()
        {
            CreateMap<Detail, DetailEntity>().ReverseMap();
            CreateMap<PagedData<DetailEntity>, PagedData<Detail>>();

            CreateMap<DetailImage, DetailImageEntity>().ReverseMap();
        }
    }
}