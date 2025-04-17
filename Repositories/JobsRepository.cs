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
}