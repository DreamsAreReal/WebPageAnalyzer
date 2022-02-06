using MongoDB.Driver;
using Quartz;
using WebPageAnalyzer.Business;
using WebPageAnalyzer.Business.Observers;
using WebPageAnalyzer.Core.Options;
using WebPageAnalyzer.Storage;
using WebPageAnalyzer.Storage.Dto;
using ConfigurationManager = Microsoft.Extensions.Configuration.ConfigurationManager;

namespace WebPageAnalyzer.Extensions;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection Setup(this IServiceCollection collection, ConfigurationManager configuration)
    {
        collection.AddControllers();
        collection.AddEndpointsApiExplorer();
        collection.AddSwaggerGen();


        // Configuration
        collection.Configure<QuartzOptions>(configuration.GetSection("Quartz"));
        collection.Configure<MongoOptions>(configuration.GetSection("Mongo"));
        
        
        // MongoDB
        if (!string.IsNullOrWhiteSpace(configuration.GetSection("Mongo").Get<MongoOptions>().Connection))
            collection.AddSingleton<IMongoClient>(new MongoClient(configuration.GetSection("Mongo").Get<MongoOptions>().Connection));

        // Quartz
        collection.AddQuartz(q =>
        {
            q.UseMicrosoftDependencyInjectionJobFactory();
            q.UseSimpleTypeLoader();
            q.UseInMemoryStore();
            q.UseDefaultThreadPool(tp => { tp.MaxConcurrency = 20; });
        });

        // AutoMapper
        collection.AddAutoMapper(e=>e.Setup());
        
        // Add service
        collection.AddSingleton<Notification<ResultDto>>();
        collection.AddSingleton<Notification<TaskDto>>();
  
        collection.AddScoped<Repository<TaskDto>>();
        collection.AddScoped<Repository<ResultDto>>();
        collection.AddScoped<JobFactory>();


        return collection;
    }
}