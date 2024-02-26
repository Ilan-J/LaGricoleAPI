namespace LaGricoleAPI.Models;

public class Employee
{
    public int Id                   { get; set; }
    public string Lastname          { get; set; }
    public string Firstname         { get; set; }
    public string Phone             { get; set; }
    public string? Cellphone        { get; set; }
    public string Email             { get; set; }
    public Department Department    { get; set; }
    public Location Location        { get; set; }
}
