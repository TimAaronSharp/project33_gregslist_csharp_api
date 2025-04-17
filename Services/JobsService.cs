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
}