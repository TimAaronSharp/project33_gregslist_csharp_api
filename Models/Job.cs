namespace System.ComponentModel.DataAnnotations;

public class Job
{
  public int Id { get; set; }
  public string CompanyName { get; set; }
  public string JobTitle { get; set; }
  public int? Salary { get; set; }
  public string Description { get; set; }
  public string SiteLocation { get; set; }
  public string CompanyHeadquarters { get; set; }
  public bool? IsRemote { get; set; }
  public bool? Sucks { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
  public string CreatorId { get; set; }
  public Account Creator { get; set; }
}