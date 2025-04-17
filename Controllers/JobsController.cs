using System.ComponentModel.DataAnnotations;

namespace gregslist_dotnet.Controllers;

[ApiController]
[Route("api/[controller]")]

public class JobsController : ControllerBase
{
  public JobsController(JobsService jobsService, Auth0Provider auth0Provider)
  {
    _jobsService = jobsService;
    _auth0Provider = auth0Provider;
  }

  private readonly JobsService _jobsService;
  private readonly Auth0Provider _auth0Provider;

  [HttpGet]
  public ActionResult<List<Job>> GetAllJobs()
  {
    try
    {
      List<Job> jobs = _jobsService.GetAllJobs();
      return Ok(jobs);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [HttpGet("{jobId}")]
  public ActionResult<Job> GetJobById(int jobId)
  {
    try
    {
      Job job = _jobsService.GetJobById(jobId);
      return Ok(job);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [Authorize]
  [HttpPost]

  public async Task<ActionResult<Job>> CreateJob([FromBody] Job jobData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      jobData.CreatorId = userInfo.Id;
      Job job = _jobsService.CreateJob(jobData, userInfo);
      return Ok(job);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }
}