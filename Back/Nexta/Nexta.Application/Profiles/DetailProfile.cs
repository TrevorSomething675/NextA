using AutoMapper;
using Nexta.Application.Commands.Admin.CreateAdminDetailCommand;
using Nexta.Application.Commands.Admin.UpdateDetailCommand;
using Nexta.Application.DTO.Admin;
using Nexta.Application.DTO.Request;
using Nexta.Application.DTO.Response;
using Nexta.Domain.Enums;
using Nexta.Domain.Models;
using Nexta.Domain.Models.DataModels;
using Nexta.Domain.Models.Images;

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
            CreateMap<DetailImageRequest, DetailImage>();
            CreateMap<DetailImage, DetailImageResponse>();
            CreateMap<DetailImage, AdminDetailImageResponse>();
            CreateMap<CreateAdminDetailCommandRequest, Detail>();
        }
    }
}