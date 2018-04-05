using AutoMapper;
using DomainModel;
using Microsoft.Extensions.DependencyInjection;
using WebService.Models.Post;
using WebService.Models.Question;
using WebService.Models.Note;

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
                cfg.CreateMap<Note, NoteModel>().ReverseMap();

            });
            return config.CreateMapper();
        }
    }
}
