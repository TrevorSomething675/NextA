using Nexta.Application.DTO;
using Nexta.Domain.Models;
using AutoMapper;
using Nexta.Domain.Models.DataModels;

namespace Nexta.Application.Profiles
{
    public class DetailProfile : Profile
    {
        public DetailProfile() 
        {
            CreateMap<Detail, DetailResponse>();
            CreateMap<PagedData<Detail>, PagedData<DetailResponse>>();
        }
    }
}