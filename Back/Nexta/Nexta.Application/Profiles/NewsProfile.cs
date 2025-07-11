using Nexta.Application.Commands.Admin.AddNewsCommand;
using Nexta.Domain.Models;
using AutoMapper;

namespace Nexta.Application.Profiles
{
    public class NewsProfile : Profile
    {
        public NewsProfile() 
        {
            CreateMap<AddNewsCommandRequest, News>();
        }
    }
}