using LaGricoleAPI.Models;

namespace LaGricoleAPI.Utils.Mappers;

public static class MapperLocationWithLocationDTO
{
    public static LocationDTO LocationToDTO(Location location)
    {
        return new LocationDTO()
        {
            Pk_location = location.Id,
            City        = location.City
        };
    }
    public static Location LocationFromDTO(LocationDTO dto)
    {
        return new Location()
        {
            Id      = dto.Pk_location,
            City    = dto.City
        };
    }
    public static List<Location> LocationsFromDTOS(List<LocationDTO> dtos)
    {
        List<Location> locations = new();

        foreach (LocationDTO dto in dtos)
            locations.Add(LocationFromDTO(dto));
            
        return locations;
    }
}