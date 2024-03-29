using LaGricoleAPI.Models;
using LaGricoleAPI.Repositories;

namespace LaGricoleAPI.Services;

public static class EmployeesService
{

	private static void Completer(Employee employee)
	{
		if (employee.Department != null)
			employee.Department = DepartementsRepository.Select(employee.Department.Id);
			
		if (employee.Location != null)
			employee.Location = LocationsRepository.Select(employee.Location.Id);
	}

	public static List<Employee> GetAll()
	{
		List<Employee> employees = EmployeesRepository.Select();
		foreach (Employee employee in employees)
			Completer(employee);

		return employees;
	}

	public static Employee? Get(int id)
	{
		Employee? employee = EmployeesRepository.Select(id);
		if (employee != null)
			Completer(employee);
		
		return employee;
	}

	public static Employee? Insert(EmployeeNew employeeNew)
	{
		int? id = EmployeesRepository.Insert(employeeNew);
		if (id == null)
			return null;

		Employee? employee = EmployeesRepository.Select((int)id);
		if (employee != null)
			Completer(employee);

		return employee;
	}

	public static bool Update(int id, EmployeeNew employeeNew)
	{
		return EmployeesRepository.Update(id, employeeNew);
	}

	public static bool Delete(int id)
	{
		return EmployeesRepository.Delete(id);
	}
}