using Nexta.Domain.Entities;
using Nexta.Domain.Models;
using AutoMapper;

namespace Nexta.Application.Mappings
{
    public class NewsMapping : Profile
    {
        public NewsMapping() 
        {
            CreateMap<News, NewsEntity>()
                .ReverseMap();
        }
    }
}