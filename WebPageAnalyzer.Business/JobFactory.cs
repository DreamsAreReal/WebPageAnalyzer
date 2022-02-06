using Quartz;
using Quartz.Impl.Matchers;

namespace WebPageAnalyzer.Business;

public class JobFactory
{
    private IScheduler _scheduler;
    
    public JobFactory(ISchedulerFactory schedulerFactory)
    {
        _scheduler = schedulerFactory.GetScheduler().Result;
    }

    public void Add<T>(string url, string cronExpression) where T : IJob
    {
        IJobDetail job = JobBuilder.Create<T>()
            .WithIdentity(url)
            .Build();
		
        // Trigger the job to run now, and then every 40 seconds
        ITrigger trigger = TriggerBuilder.Create()
            .WithIdentity(url)
            .WithCronSchedule("0 0/2 8-17 * * ?")
            .ForJob(url)
            .Build();
        
        _scheduler.ScheduleJob(job, trigger);
    }

    public void Remove(string url)
    {
        _scheduler.DeleteJob(new JobKey(url));
    }

    
}