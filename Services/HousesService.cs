using System.ComponentModel.DataAnnotations;

namespace gregslist_dotnet.Services;

public class HousesService
{

  public HousesService(HousesRepository housesRepository)
  {
    _housesRepository = housesRepository;
  }
  private readonly HousesRepository _housesRepository;

  public List<House> GetAllHouses()
  {
    List<House> houses = _housesRepository.GetAllHouses();
    return houses;
  }

  internal House GetHouseById(int houseId)
  {
    House house = _housesRepository.GetHouseById(houseId);
    if (house == null)
    {
      throw new Exception($"No house found with the id of {houseId}");
    }
    return house;
  }

  internal House CreateHouse(House houseData)
  {
    House house = _housesRepository.CreateHouse(houseData);
    return house;
  }

  internal House UpdateHouse(int houseId, House updateHouseData, Account userInfo)
  {
    House house = GetHouseById(houseId);

    if (house.CreatorId != userInfo.Id)
    {
      throw new Exception($"You cannot update a listing that you did not create, {userInfo.Name}".ToUpper());
    }

    house.Sqft = updateHouseData.Sqft ?? house.Sqft;
    house.Bedrooms = updateHouseData.Bedrooms ?? house.Bedrooms;
    house.Bathrooms = updateHouseData.Bathrooms ?? house.Bathrooms;
    house.ImgUrl = updateHouseData.ImgUrl ?? house.ImgUrl;
    house.Description = updateHouseData.Description ?? house.Description;
    house.Price = updateHouseData.Price ?? house.Price;
    house.SomeoneDied = updateHouseData.SomeoneDied ?? house.SomeoneDied;
    house.IsHaunted = updateHouseData.IsHaunted ?? house.IsHaunted;

    _housesRepository.UpdateHouse(house);

    return house;


  }
}