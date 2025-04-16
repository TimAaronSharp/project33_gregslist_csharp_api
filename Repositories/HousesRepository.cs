using System.ComponentModel.DataAnnotations;

namespace gregslist_dotnet.Repositories;

public class HousesRepository
{

  public HousesRepository(IDbConnection db)
  {
    _db = db;
  }
  private readonly IDbConnection _db;

  public List<House> GetAllHouses()
  {
    string sql = @"
    SELECT 
    * 
    FROM houses";

    List<House> houses = _db.Query<House>(sql).ToList();
    return houses;
  }
}