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

  [Authorize]
  [HttpPut("{jobId}")]
  public async Task<ActionResult<Job>> UpdateJob(int jobId, [FromBody] Job updateJobData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      Job job = _jobsService.UpdateJob(jobId, updateJobData, userInfo);
      return Ok(job);

    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [Authorize]
  [HttpDelete("{jobId}")]

  public async Task<ActionResult<string>> DeleteJob(int jobId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      string deleteMessage = _jobsService.DeleteJob(jobId, userInfo);
      return Ok(deleteMessage);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }
}