using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

[ApiController]
[Route("[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IStorageService _storageService;

    public ProjectsController(IStorageService storageService)
    {
        _storageService = storageService;
    }

    // GET: api/Projects
    [HttpGet]
    public IActionResult GetAllProjects()
    {
        IEnumerable<Project> projects = _storageService.GetAllProjects();
        return Ok(projects);
    }

    // GET: api/Projects/{id}
    [HttpGet("{id}")]
    public IActionResult GetProjectById(string id)
    {
        Project project = _storageService.GetProjectById(id);
        if (project == null)
        {
            return NotFound();
        }
        return Ok(project);
    }

    // POST: api/Projects
    [HttpPost]
    public IActionResult AddProject([FromBody] Project project)
    {
        bool projectExists = _storageService.ProjectExists(project.Name);

        if (projectExists)
        {
            return Conflict(new { message = "A project with the same name already exists." });
        }

        _storageService.AddProject(project);
        return CreatedAtAction(nameof(GetProjectById), new { id = project.Id }, project);
    }

    // PUT: api/Projects/{id}
    [HttpPut("{id}")]
    public IActionResult EditProject(string id, [FromBody] Project project)
    {
        if (id != project.Id)
        {
            return BadRequest();
        }

        try
        {
            _storageService.EditProject(project);
            return NoContent();
        }
        catch
        {
            return NotFound();
        }
    }

    // DELETE: api/Projects/{id}
    [HttpDelete("{id}")]
    public IActionResult DeleteProject(string id)
    {
        bool isDeleted = _storageService.DeleteProject(id);

        if (!isDeleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}