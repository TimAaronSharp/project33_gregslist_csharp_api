namespace System.ComponentModel.DataAnnotations;

public class House
{
  public int Id { get; set; }
  public int Sqft { get; set; }
  public int Bedrooms { get; set; }
  public double Bathrooms { get; set; }
  [MaxLength(1000)] public string ImgUrl { get; set; }
  [MaxLength(500)] public string Description { get; set; }
  public int? Price { get; set; } //NOTE added null check(?) to allow someone listing for $0
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
  public bool SomeoneDied { get; set; }
  public bool IsHaunted { get; set; }
  public string CreatorId { get; set; }
  public Account Creator { get; set; }
}