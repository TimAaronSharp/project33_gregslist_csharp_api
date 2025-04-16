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

  }
}