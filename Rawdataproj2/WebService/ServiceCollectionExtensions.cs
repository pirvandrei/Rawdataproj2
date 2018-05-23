using DataService;
using Microsoft.Extensions.DependencyInjection;
using StackoverflowContext;

namespace WebService
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<ISearchHistoryRepository, SearchHistoryRepository>();
            //services.AddSingleton<IPostRepository, PostRepository>();
            services.AddSingleton<IQuestionRepository, QuestionRepository>();
            services.AddSingleton<INoteRepository, NoteRepository>();
            services.AddSingleton<IBookmarkRepository, BookmarkRepository>();
            services.AddSingleton<ISearchRepository, SearchRepository>();
            services.AddSingleton<IStatisticsRepository, StatisticsRepository>();
            return services;
        }
    }
}
