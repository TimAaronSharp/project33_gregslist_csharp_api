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

  internal House CreateHouse(House houseData)
  {
    string sql = @"
    INSERT INTO 
    houses (sqft, bedrooms, bathrooms, img_url, description, price, someone_died, is_haunted, creator_id)
    VALUES (@Sqft, @Bedrooms, @Bathrooms, @ImgUrl, @Description, @Price, @SomeoneDied, @IsHaunted, @CreatorId);
    
    SELECT
    houses.*,
    accounts.*
    FROM houses
    INNER JOIN accounts ON accounts.id = houses.creator_id
    WHERE houses.id = LAST_INSERT_ID();";

    House createdHouse = _db.Query(sql, (House house, Account account) =>
    {
      house.Creator = account;
      return house;
    }, houseData).SingleOrDefault();
    return createdHouse;
  }

  internal void UpdateHouse(House house)
  {
    string sql = @"
    UPDATE houses
    SET
    sqft = @Sqft,
    bedrooms = @Bedrooms,
    bathrooms = @Bathrooms,
    img_url = @ImgUrl,
    description = @Description,
    price = @Price,
    someone_died = @SomeoneDied,
    is_haunted = @IsHaunted
    WHERE id = @Id
    LIMIT 1;";

    int rowsAffected = _db.Execute(sql, house);

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