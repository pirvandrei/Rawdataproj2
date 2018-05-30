using AutoMapper;
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
                cfg.CreateMap<Question, QuestionModel>() 
                            .ForMember(d => d.UserName, m => m.MapFrom(s => s.User.DisplayName)) 
				            .ForMember(d => d.Bookmarked, m => m.MapFrom(s => s.Bookmarks.Count != 0))
                            .ReverseMap();
                cfg.CreateMap<Comment, CommentModel>()
                            .ForMember(d => d.UserName, m => m.MapFrom(s => s.User.DisplayName)) 
                            .ReverseMap();
                cfg.CreateMap<Answer, AnswerModel>()
                            .ForMember(d => d.UserName, m => m.MapFrom(s => s.User.DisplayName)) 
			            	.ForMember(d => d.Bookmarked, m => m.MapFrom(s => s.Bookmarks.Count != 0))
                            .ReverseMap();
                cfg.CreateMap<Note, NoteModel>()
                            .ReverseMap();
                cfg.CreateMap<Bookmark, BookmarkModel>() 
				            .ReverseMap();             
                cfg.CreateMap<RankedWordListDto, RankedWordListModel>()
                            .ForMember(r => r.Weight, m => m.MapFrom(s => s.Rank))
                            .ForMember(r => r.Text, m => m.MapFrom(s => s.Word))
                            .ReverseMap();
                cfg.CreateMap<WeightedWordListDto, WeightedWordListModel>()
				            .ReverseMap();
                cfg.CreateMap<AssociationsListDto, AssociationsListModel>()
				            .ReverseMap();
                cfg.CreateMap<TermNetworkDto, TermNetworkModel>()
				            .ReverseMap(); 
            });
            return config.CreateMapper();
        }
    }
}
