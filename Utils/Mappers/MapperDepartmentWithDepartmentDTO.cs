using LaGricoleAPI.Models;

namespace LaGricoleAPI.Utils.Mappers;

public static class MapperDepartmentWithDepartmentDTO
{
    public static DepartmentDTO DepartmentToDTO(Department department)
    {
        return new DepartmentDTO()
        {
            Pk_department   = department.Id,
            Name            = department.Name
        };
    }
    public static Department DepartmentFromDTO(DepartmentDTO dto)
    {
        return new Department()
        {
            Id      = dto.Pk_department,
            Name    = dto.Name
        };
    }
    public static List<Department> DepartmentsFromDTOS(List<DepartmentDTO> dtos)
    {
        List<Department> departments = new();

        foreach (DepartmentDTO dto in dtos)
            departments.Add(DepartmentFromDTO(dto));
            
        return departments;
    }
}