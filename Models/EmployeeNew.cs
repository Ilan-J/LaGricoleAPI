namespace LaGricoleAPI.Models;

public class EmployeeNew
{
    public string Lastname  { get; set; }
    public string Firstname { get; set; }
    public string Phone     { get; set; }
    public string Cellphone { get; set; }
    public string Email     { get; set; }
    public int DepartmentId { get; set; }
    public int LocationId   { get; set; }
}
