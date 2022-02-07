using NLog.Web;
using WebPageAnalyzer.Business;
using WebPageAnalyzer.Business.Jobs;
using WebPageAnalyzer.Business.Observers;
using WebPageAnalyzer.Extensions;
using WebPageAnalyzer.Middlewares;
using WebPageAnalyzer.Storage;
using WebPageAnalyzer.Storage.Dto;

var builder = WebApplication.CreateBuilder(args);

// Add configuration.
builder.Configuration.Setup();

// Add services to the container.
builder.Services.Setup(builder.Configuration);
builder.Host.UseNLog();
builder.Logging.SetMinimumLevel(LogLevel.Trace);


var app = builder.Build();


// Publisher
IObservable<TaskDto> taskPublisher = app.Services.GetService<Publisher<TaskDto>>();
IObservable<string> taskRemover = app.Services.GetService<Publisher<string>>();

taskPublisher.Subscribe(app.Services.GetService<TaskToDatabaseAppendObserver>());
taskPublisher.Subscribe(app.Services.GetService<TaskToWorkersAppendObserver>());

taskRemover.Subscribe(app.Services.GetService<TaskFromDatabaseRemoveObserver>());
taskRemover.Subscribe(app.Services.GetService<TaskFromWorkersRemoveObserver>());


// Start jobs
var repository = app.Services.GetService<Repository<TaskDto>>();
var factory = app.Services.GetService<JobFactory>();
var tasks = repository.Get().Result;

foreach (var task in tasks) factory.Add<WordsCountJob>(task);


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();