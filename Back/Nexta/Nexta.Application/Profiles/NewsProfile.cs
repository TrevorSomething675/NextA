using Nexta.Application.Commands.Admin.AddNewsCommand;
using Nexta.Application.DTO.Response;
using Nexta.Domain.Models.News;
using AutoMapper;

namespace Nexta.Application.Profiles
{
    public class NewsProfile : Profile
    {
        public NewsProfile() 
        {
            CreateMap<AddNewsCommand, News>();
            CreateMap<News, NewsResponse>();
        }
    }
}