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

  internal void UpdateJob(Job job)
  {
    string sql = @"
    UPDATE jobs
    SET
    company_name = @CompanyName,
    job_title = @JobTitle,
    salary = @Salary,
    description = @Description,
    site_location = @SiteLocation,
    company_headquarters = @CompanyHeadquarters,
    is_remote = @IsRemote,
    sucks = @Sucks,
    creator_id = @CreatorId
    WHERE id = @Id
    LIMIT 1;";

    int rowsAffected = _db.Execute(sql, job);

    if (rowsAffected == 0)
    {
      throw new Exception("No rows were updated".ToUpper());
    }

    if (rowsAffected > 1)
    {
      throw new Exception($"{rowsAffected} rows were updated which is really bad and means your code is bad and you should feel bad.".ToUpper());
    }
  }
}