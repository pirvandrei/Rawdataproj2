using AutoMapper;
using DataRepository.Dto.PostDto;
using DataRepository.Dto.QuestionDto;
using DomainModel;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService.Models.Post;
using WebService.Models.Question;

namespace WebService
{
    public static class MapperCollectionExtension
    {
        public static IServiceCollection RegisterMappers(this IServiceCollection services)
        {

            services.AddSingleton<IMapper>(CreateMapper());

            return services;
        }

        public static IMapper CreateMapper()
        {
            var config = new MapperConfiguration(cfg =>
            { 
                cfg.CreateMap<Post, PostModel>().ReverseMap();
                cfg.CreateMap<Question, QuestionModel>().ReverseMap();

            });
            return config.CreateMapper();
        }
    }
}
