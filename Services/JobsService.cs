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
}