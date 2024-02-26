using LaGricoleAPI.Models;
using LaGricoleAPI.Repositories;

namespace LaGricoleAPI.Services;

public static class DepartmentsService
{
	public static List<Department> GetAll()
	{
		return DepartementsRepository.Select();
	}

	public static Department? Get(int id)
	{
		return DepartementsRepository.Select(id);
	}

	public static Department? Insert(DepartmentNew departmentNew)
	{
		int? id = DepartementsRepository.Insert(departmentNew);

		return id == null ? null : DepartementsRepository.Select((int)id);
	}

	public static bool Update(int id, DepartmentNew departmentNew)
	{
		return DepartementsRepository.Update(id, departmentNew);
	}

	public static bool Delete(int id)
	{
		return DepartementsRepository.Delete(id);
	}
}