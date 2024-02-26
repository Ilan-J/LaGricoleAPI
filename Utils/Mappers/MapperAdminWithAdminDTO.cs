using LaGricoleAPI.Models;

namespace LaGricoleAPI.Utils.Mappers;

public static class MapperAdminWithAdminDTO
{
	public static AdminDTO AdminToDTO(Admin admin)
	{
		return new AdminDTO()
		{
			Pk_employee = admin.Id,
			Email		= admin.Email,
			Password	= admin.Password
		};
	}
	public static Admin AdminFromDTO(AdminDTO dto)
	{
		return new Admin()
		{
			Id			= dto.Pk_employee,
			Email		= dto.Email,
			Password	= dto.Password
		};
	}
	public static List<Admin> AdminsFromDTOS(List<AdminDTO> dtos)
	{
		List<Admin> admins = new();

		foreach (AdminDTO dto in dtos)
			admins.Add(AdminFromDTO(dto));

		return admins;
	}
}