using Quartz;
using WebPageAnalyzer.Storage.Dto;

namespace WebPageAnalyzer.Business;

public class JobFactory
{
    private readonly IScheduler _scheduler;

    public JobFactory(ISchedulerFactory schedulerFactory)
    {
        _scheduler = schedulerFactory.GetScheduler().Result;

        if (!_scheduler.IsStarted)
            _scheduler.Start().Wait();
    }


    public void Add<T>(TaskDto model) where T : IJob
    {
        var data = new JobDataMap
        {
            ["data"] = model
        };

        var job = JobBuilder.Create<T>()
            .WithIdentity(model.Url)
            .SetJobData(data)
            .Build();

        var trigger = TriggerBuilder.Create()
            .WithIdentity(model.Url)
            .WithCronSchedule(model.CronExpression)
            .ForJob(model.Url)
            .Build();

        _scheduler.ScheduleJob(job, trigger);
    }

    public void Remove(string url)
    {
        _scheduler.PauseJob(new JobKey(url));
        _scheduler.DeleteJob(new JobKey(url));
    }
}