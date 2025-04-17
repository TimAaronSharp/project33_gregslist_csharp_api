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
    houses.*,
    accounts.* 
    FROM houses
    INNER JOIN accounts ON accounts.id = houses.creator_id";

    // NOTE .Query is a dapper method that will make the query for us. We need to tell it the datatype we expect to be returned (<House>) and pass it "sql". When passing Query this stringified sql command dapper is able to "sanitize" the request for us to protect from malicious sql injection attacks. If we just used string interpolation then someone could use sql commands in the url request to perform malicious actions (drop table, edit/copy sensitive data such as credit card/social security numbers, etc.). The data returned needs to be converted to a type that our code can use. In this case a List of Houses. ToList() will do this for us.

    // NOTE
    List<House> houses = _db.Query(sql, (House house, Account account) =>
    {
      house.Creator = account;
      return house;
    }).ToList();
    return houses;
  }

  internal House GetHouseById(int houseId)
  {
    string sql = @"
    SELECT 
    houses.*,
    accounts.* 
    FROM houses
    INNER JOIN accounts ON accounts.id = houses.creator_id 
    WHERE houses.id = @houseId";
    // NOTE We need to send our houseId along with the sql string for it to be able to find what we are looking for. It needs us to send an object, so we can create a new object with a key:value pair of houseId: houseId (if you just write the key name it will automatically make the value whatever that key holds, in this case the id of the house). SingleOrDefault() converts what it returns into a single thing, or default (null/0) if not found.

    House house = _db.Query(sql, (House house, Account account) =>
    {
      house.Creator = account;
      return house;
    }, new { houseId }).SingleOrDefault();
    return house;
  }
}



// NOTE Copied these methods down here for reference on how to do them without using/joining with the account table info.

// public List<House> GetAllHouses()
//   {
//     string sql = @"
//     SELECT 
//     * 
//     FROM houses";

//     // NOTE .Query is a dapper method that will make the query for us. We need to tell it the datatype we expect to be returned (<House>) and pass it "sql". When passing Query this stringified sql command dapper is able to "sanitize" the request for us to protect from malicious sql injection attacks. If we just used string interpolation then someone could use sql commands in the url request to perform malicious actions (drop table, edit/copy sensitive data such as credit card/social security numbers, etc.). The data returned needs to be converted to a type that our code can use. In this case a List of Houses. ToList() will do this for us.

//     // NOTE
//     List<House> houses = _db.Query<House>(sql).ToList();
//     return houses;
//   }

//   internal House GetHouseById(int houseId)
//   {
//     string sql = @"
//     SELECT 
//     * 
//     FROM houses 
//     WHERE houses.id = @houseId";
//     // NOTE We need to send our houseId along with the sql string for it to be able to find what we are looking for. It needs us to send an object, so we can create a new object with a key:value pair of houseId: houseId (if you just write the key name it will automatically make the value whatever that key holds, in this case the id of the house). SingleOrDefault() converts what it returns into a single thing, or default (null/0) if not found.

//     House house = _db.Query<House>(sql, new { houseId }).SingleOrDefault();
//     return house;
//   }