using Microsoft.AspNetCore.Mvc;
using LaGricoleAPI.Models;
using LaGricoleAPI.Services;

namespace LaGricoleAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class DepartmentsController : ControllerBase
{
	private readonly ILogger<DepartmentsController> _logger;

	public DepartmentsController(ILogger<DepartmentsController> logger)
	{
		_logger = logger;
	}

	// GET: Departments
	[HttpGet]
	public ActionResult<IEnumerable<Department>> GetDepartements()
	{
		List<Department> departments = DepartmentsService.GetAll();

		return Ok(departments);
	}

	// GET: Departments/5
	[HttpGet("{id}")]
	public ActionResult<Department> GetDepartment(int id)
	{
		Department? department = DepartmentsService.Get(id);
		if (department == null)
			return NotFound();

		return Ok(department);
	}

	// POST: Departments
	// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
	[HttpPost]
	public ActionResult<Department> PostDepartment(DepartmentNew departmentNew)
	{
		Department? department = DepartmentsService.Insert(departmentNew);
		if (department == null)
			return BadRequest();

		return CreatedAtAction(nameof(PostDepartment), new { id = department.Id }, department);
	}

	// PUT: Departments/5
	// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
	[HttpPut("{id}")]
	public IActionResult PutDepartment(int id, DepartmentNew departmentNew)
	{
		if (false)
			return BadRequest();

		if (!DepartmentExists(id))
			return NotFound();

		if (!DepartmentsService.Update(id, departmentNew))
			return Conflict();

		return NoContent();
	}

	// DELETE: Departments/5
	[HttpDelete("{id}")]
	public IActionResult DeleteDepartment(int id)
	{
		if (!DepartmentExists(id))
			return NotFound();

		if (!DepartmentsService.Delete(id))
			return Conflict();

		return NoContent();
	}

	private static bool DepartmentExists(int id)
	{
		return DepartmentsService.Get(id) != null;
	}
}
