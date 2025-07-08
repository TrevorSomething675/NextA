using Nexta.Application.Commands.Admin.UpdateDetailCommand;
using Nexta.Application.DTO.RequestModels;
using Nexta.Domain.Models.DataModels;
using Nexta.Application.DTO.Admin;
using Nexta.Application.DTO;
using Nexta.Domain.Models;
using Nexta.Domain.Enums;
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
            CreateMap<UpdateAdminDetailCommandRequest, Detail>()
                .ForMember(src => src.Status, opt => opt.MapFrom(x => (DetailStatus)x.Status));
            CreateMap<DetailRequest, Detail>();
        }
    }
}