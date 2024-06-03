using Microsoft.AspNetCore.Mvc;
using LaGricoleAPI.Models;
using LaGricoleAPI.Services;

namespace LaGricoleAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeesController : ControllerBase
{
	private readonly ILogger<EmployeesController> _logger;

	public EmployeesController(ILogger<EmployeesController> logger)
	{
		_logger = logger;
	}

	// GET: Employees
	[HttpGet]
	public ActionResult<IEnumerable<Employee>> GetEmployees(
		[FromQuery] string? name = null,
		[FromQuery] int? location = null,
		[FromQuery] int? department = null
		)
	{
		List<Employee> employees = EmployeesService.GetAll(name, location, department);

		return Ok(employees);
	}

	// GET: Employees/5
	[HttpGet("{id}")]
	public ActionResult<Employee> GetEmployee(int id)
	{
		Employee? employee = EmployeesService.Get(id);
		if (employee == null)
			return NotFound();

		return Ok(employee);
	}

	// POST: Employees
	// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
	[HttpPost]
	public ActionResult<Employee> PostEmployee(EmployeeNew employeeNew)
	{
		Employee? employee = EmployeesService.Insert(employeeNew);
		if (employee == null)
			return BadRequest();

		return CreatedAtAction(nameof(PostEmployee), new { id = employee.Id }, employee);
	}

	// PUT: Employees/5
	// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
	[HttpPut("{id}")]
	public IActionResult PutEmployee(int id, EmployeeNew employeeNew)
	{
		if (false)
			return BadRequest();

		if (!EmployeeExists(id))
			return NotFound();

		if (!EmployeesService.Update(id, employeeNew))
			return Conflict();

		return NoContent();
	}

	// DELETE: Employees/5
	[HttpDelete("{id}")]
	public IActionResult DeleteEmployee(int id)
	{
		if (!EmployeeExists(id))
			return NotFound();

		if (!EmployeesService.Delete(id))
			return Conflict();

		return NoContent();
	}

	private static bool EmployeeExists(int id)
	{
	    return EmployeesService.Get(id) != null;
	}
}
