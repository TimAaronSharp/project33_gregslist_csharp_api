using System.ComponentModel.DataAnnotations;

namespace gregslist_dotnet.Repositories;

public class JobsRepository
{



  public JobsRepository(IDbConnection db)
  {
    _db = db;
  }
  private readonly IDbConnection _db;

  internal List<Job> GetAllJobs()
  {
    string sql = @"
    SELECT
    jobs.*,
    accounts.*
    FROM jobs
    INNER JOIN accounts ON accounts.id = jobs.creator_id;";

    List<Job> jobs = _db.Query(sql, (Job job, Account account) =>
    {
      job.Creator = account;
      return job;
    }).ToList();
    return jobs;
  }

  internal Job GetJobById(int jobId)
  {
    string sql = @"
    SELECT
    jobs.*,
    accounts.*
    FROM jobs
    INNER JOIN accounts ON accounts.id = jobs.creator_id
    WHERE jobs.id = @jobId;";

    Job foundJob = _db.Query(sql, (Job job, Account account) =>
    {
      job.Creator = account;
      return job;
    }, new { jobId }).SingleOrDefault();
    return foundJob;
  }

  internal Job CreateJob(Job jobData)
  {
    string sql = @"
   INSERT INTO
   jobs (company_name, job_title, salary, description, site_location, company_headquarters, is_remote, sucks, creator_id)
   VALUES (@CompanyName, @JobTitle, @Salary, @Description, @SiteLocation, @CompanyHeadquarters, @IsRemote, @Sucks, @CreatorId);
   
   SELECT
   jobs.*,
   accounts.*
   FROM jobs
   INNER JOIN accounts ON accounts.id = jobs.creator_id
   WHERE jobs.id = LAST_INSERT_ID();";

    Job createdJob = _db.Query(sql, (Job job, Account account) =>
    {
      job.Creator = account;
      return job;
    }, jobData).SingleOrDefault();

    return createdJob;

  }
}