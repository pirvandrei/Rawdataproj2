using AutoMapper;
using DataRepository.Dto.PostDto;
using DomainModel;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService.Models.Post;

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

                cfg.CreateMap<PostDto, PostModel>().ReverseMap();

            });
            return config.CreateMapper();
        }
    }
}
