using Microsoft.AspNetCore.Mvc;
using LaGricoleAPI.Models;
using LaGricoleAPI.Services;

namespace LaGricoleAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class LocationsController : ControllerBase
{
	private readonly ILogger<LocationsController> _logger;

	public LocationsController(ILogger<LocationsController> logger)
	{
		_logger = logger;
	}

	// GET: Locations
	[HttpGet]
	public ActionResult<IEnumerable<Location>> GetLocations()
	{
		List<Location> locations = LocationsService.GetAll();

		return Ok(locations);
	}

	// GET: Locations/5
	[HttpGet("{id}")]
	public ActionResult<Location> GetLocation(int id)
	{
		Location? location = LocationsService.Get(id);
		if (location == null)
			return NotFound();

		return Ok(location);
	}

	// POST: Locations
	// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
	[HttpPost]
	public ActionResult<Location> PostLocation(LocationNew locationNew)
	{
		Location? location = LocationsService.Insert(locationNew);
		if (location == null)
			return BadRequest();

		// return CreatedAtAction("GetLocation", new { id = location.Id }, location);
		return CreatedAtAction(nameof(PostLocation), new { id = location.Id }, location);
	}

	// PUT: Locations/5
	// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
	[HttpPut("{id}")]
	public IActionResult PutLocation(int id, LocationNew locationNew)
	{
		if (false)
			return BadRequest();

		if (!LocationExists(id))
			return NotFound();

		if (!LocationsService.Update(id, locationNew))
			return Conflict();

		return NoContent();
	}

	// DELETE: Locations/5
	[HttpDelete("{id}")]
	public IActionResult DeleteLocation(int id)
	{
		if (!LocationExists(id))
			return NotFound();

		if (!LocationsService.Delete(id))
			return Conflict();

		return NoContent();
	}

	private static bool LocationExists(int id)
	{
		return LocationsService.Get(id) != null;
	}
}
