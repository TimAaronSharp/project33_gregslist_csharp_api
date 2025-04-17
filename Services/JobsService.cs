using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http.HttpResults;

namespace gregslist_dotnet.Services;

public class JobsService
{

  public JobsService(JobsRepository jobsRepository)
  {
    _jobsRepository = jobsRepository;
  }
  private readonly JobsRepository _jobsRepository;
  internal List<Job> GetAllJobs()
  {
    List<Job> jobs = _jobsRepository.GetAllJobs();
    return jobs;
  }

  internal Job GetJobById(int jobId)
  {
    Job job = _jobsRepository.GetJobById(jobId);
    return job;
  }

  internal Job CreateJob(Job jobData, Account userInfo)
  {

    Job job = _jobsRepository.CreateJob(jobData);
    return job;
  }

  internal Job UpdateJob(int jobId, Job updateJobData, Account userInfo)
  {
    Job job = GetJobById(jobId);

    if (job.CreatorId != userInfo.Id)
    {
      throw new Exception($"You are not allowed to update a listing you did not create, {userInfo.Name}.".ToUpper());
    }

    job.CompanyName = updateJobData.CompanyName ?? job.CompanyName;
    job.JobTitle = updateJobData.JobTitle ?? job.JobTitle;
    job.Salary = updateJobData.Salary ?? job.Salary;
    job.Description = updateJobData.Description ?? job.Description;
    job.SiteLocation = updateJobData.SiteLocation ?? job.SiteLocation;
    job.CompanyHeadquarters = updateJobData.CompanyHeadquarters ?? job.CompanyHeadquarters;
    job.IsRemote = updateJobData.IsRemote ?? job.IsRemote;
    job.Sucks = updateJobData.Sucks ?? job.Sucks;

    _jobsRepository.UpdateJob(job);

    return job;

  }
}