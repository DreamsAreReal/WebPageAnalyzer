using Quartz;
using Quartz.Impl.Matchers;
using WebPageAnalyzer.Storage.Dto;

namespace WebPageAnalyzer.Business;

public class JobFactory
{
    private IScheduler _scheduler;
    
    public JobFactory(ISchedulerFactory schedulerFactory)
    {
        _scheduler = schedulerFactory.GetScheduler().Result;
    }

    public void Add<T>(TaskDto model) where T : IJob
    {
        JobDataMap data = new JobDataMap
        {
            ["data"] = model
        };

        IJobDetail job = JobBuilder.Create<T>()
            .WithIdentity(model.Url)
            .SetJobData(data)
            .Build();
        
        ITrigger trigger = TriggerBuilder.Create()
            .WithIdentity(model.Url)
            .WithCronSchedule(model.CronExpression)
            .ForJob(model.Url)
            .Build();
        
        _scheduler.ScheduleJob(job, trigger);
    }

    public void Remove(string url)
    {
        _scheduler.DeleteJob(new JobKey(url));
    }

    
}