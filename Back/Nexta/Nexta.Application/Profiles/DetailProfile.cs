using Nexta.Domain.Models.DataModels;
using Nexta.Application.DTO.Admin;
using Nexta.Application.DTO;
using Nexta.Domain.Models;
using AutoMapper;

namespace Nexta.Application.Profiles
{
    public class DetailProfile : Profile
    {
        public DetailProfile() 
        {
            CreateMap<Detail, DetailResponse>();
            CreateMap<Detail, AdminDetailResponse>();
            CreateMap<PagedData<Detail>, PagedData<AdminDetailResponse>>();
            CreateMap<PagedData<Detail>, PagedData<DetailResponse>>();
        }
    }
}