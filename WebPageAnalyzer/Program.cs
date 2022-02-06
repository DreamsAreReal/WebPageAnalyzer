using System.Text.RegularExpressions;
using Quartz;
using Quartz.Impl;
using WebPageAnalyzer;
using WebPageAnalyzer.Analyzer;
using WebPageAnalyzer.Analyzer.Parsers;
using WebPageAnalyzer.Analyzer.TextProcessors;
using WebPageAnalyzer.Business;
using WebPageAnalyzer.Business.Observers;
using WebPageAnalyzer.Extensions;
using WebPageAnalyzer.Storage.Dto;

var builder = WebApplication.CreateBuilder(args);

// Add configuration.
builder.Configuration.Setup();

// Add services to the container.
builder.Services.Setup(builder.Configuration);


var app = builder.Build();



// Publisher
IObservable<TaskDto> taskPublisher = app.Services.GetService<Notification<TaskDto>>();
IObservable<string> taskRemover = app.Services.GetService<Notification<string>>();

taskPublisher.Subscribe(app.Services.GetService<TaskToDatabaseAppendObserver>());
taskPublisher.Subscribe(app.Services.GetService<TaskToWorkersAppendObserver>());

taskRemover.Subscribe(app.Services.GetService<TaskFromDatabaseRemoveObserver>());
taskRemover.Subscribe(app.Services.GetService<TaskFromWorkersRemoveObserver>());



// Start jobs
app.Services.GetService<ISchedulerFactory>().GetScheduler().Result.Start();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();





 