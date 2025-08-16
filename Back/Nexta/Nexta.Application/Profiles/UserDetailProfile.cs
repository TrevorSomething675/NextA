using Nexta.Application.Commands.Basket.UpdateBasketDetailCommand;
using Nexta.Domain.Models;
using AutoMapper;
using Nexta.Application.DTO.Response;

namespace Nexta.Application.Profiles
{
    public class UserDetailProfile : Profile
    {
        public UserDetailProfile() 
        {
            CreateMap<UserDetail, UserDetailResponse>();
            CreateMap<UpdateBasketDetailCommandRequest, UserDetail>();
            CreateMap<UserDetail, UpdateBasketDetailCommandResponse>();
        }
    }
}