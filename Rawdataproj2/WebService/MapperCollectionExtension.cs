﻿using AutoMapper;
using DomainModel;
using Microsoft.Extensions.DependencyInjection;
//using WebService.Models.Post;
using WebService.Models.Question;
using WebService.Models.Note;
using WebService.Models.Bookmark;
using WebService.Models.Search;
using DataService.Dto.SearchDto;
using WebService.Models.Statistics;
using DataService.Dto.StatisticsDto;

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
                //cfg.CreateMap<Post, PostModel>().ReverseMap();
                cfg.CreateMap<Question, QuestionModel>().ReverseMap();
                cfg.CreateMap<Note, NoteModel>().ReverseMap();
                cfg.CreateMap<Bookmark, BookmarkModel>().ReverseMap();
                //cfg.CreateMap<BestmatchDto, BestmatchModel>().ReverseMap();
                cfg.CreateMap<RankedWordListDto, RankedWordListModel>().ReverseMap();
                cfg.CreateMap<WeightedWordListDto, WeightedWordListModel>().ReverseMap();
                cfg.CreateMap<AssociationsListDto, AssociationsListModel>().ReverseMap();

            });
            return config.CreateMapper();
        }
    }
}
