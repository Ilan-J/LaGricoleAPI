using LaGricoleAPI.Models;

namespace LaGricoleAPI.Utils.Mappers;

public static class MapperEmployeeWithEmployeeDTO
{
	public static EmployeeDTO EmployeeToDTO(Employee employee)
	{
		return new EmployeeDTO()
		{
			Pk_employee	= employee.Id,
			Lastname	= employee.Lastname,
			Firstname	= employee.Firstname,
			Phone		= employee.Phone,
			Cellphone	= employee.Cellphone,
			Email		= employee.Email,
			Fk_department	= employee.Department.Id,
			Fk_location		= employee.Location.Id
		};
	}
	public static Employee EmployeeFromDTO(EmployeeDTO dto)
	{
		return new Employee()
		{
			Id			= dto.Pk_employee,
			Lastname	= dto.Lastname,
			Firstname	= dto.Firstname,
			Phone		= dto.Phone,
			Cellphone	= dto.Cellphone,
			Email		= dto.Email,
			Department	= new() { Id = dto.Fk_department },
			Location	= new() { Id = dto.Fk_location }
		};
	}
	public static List<Employee> EmployeesFromDTOS(List<EmployeeDTO> dtos)
	{
		List<Employee> employees = new();

		foreach (EmployeeDTO dto in dtos)
			employees.Add(EmployeeFromDTO(dto));

		return employees;
	}
}