using LaGricoleAPI.Models;
using LaGricoleAPI.Repositories;

namespace LaGricoleAPI.Services;

public static class LocationsService
{
	public static List<Location> GetAll()
	{
		return LocationsRepository.Select();
	}

	public static Location? Get(int id)
	{
		return LocationsRepository.Select(id);
	}

	public static Location? Insert(LocationNew locationNew)
	{
		int? id = LocationsRepository.Insert(locationNew);

		return id == null ? null : LocationsRepository.Select((int)id);
	}

	public static bool Update(int id, LocationNew locationNew)
	{
		return LocationsRepository.Update(id, locationNew);
	}

	public static bool Delete(int id)
	{
		return LocationsRepository.Delete(id);
	}
}