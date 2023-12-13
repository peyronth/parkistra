namespace ParkIstra.Services.MainApi.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize]
public class ProjectController : ControllerBase, IDisposable
{

    public ProjectController(
        IDbContextFactory<MainDbContext> mainDbContextFactory,
        ASPProblemDetailsFactory aspProblemDetailsFactory
    )
    {
        MainDbContext = mainDbContextFactory.CreateDbContext();
        MainDbContextFactory = mainDbContextFactory;
        ProblemFactory = aspProblemDetailsFactory;
    }

    [HttpGet]
    [OdataEnableQuery]
    //[Authorize(PolicyMainApiNames.MainApi_Project_Read)]

    public async Task<ActionResult<IQueryable<Project>>> GetAllProjects()
    {
        var query = MainDbContext.Projects;

        return Ok(query);
    }


    [HttpGet("{id:int}")]
    [OdataEnableQuery]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    //[Authorize(PolicyMainApiNames.MainApi_Project_Read)]

    public async Task<ActionResult<Project>> GetProjectById(int id)
    {
        var query = SingleResult.Create(
            MainDbContext.Projects.Where(m => m.Id == id));

        return Ok(query);
    }

    [HttpGet("count")]
    //[Authorize(PolicyMainApiNames.MainApi_Project_Read)]

    public ActionResult<IntWrapper> GetProjectCount(string? search)
    {
        if (search == null)
        {
            return Ok(new IntWrapper(MainDbContext.Projects.Count()));

        }
        else
        {
            return Ok(new IntWrapper(MainDbContext.Projects.Where(x => x.Name.Contains(search)).Count()));
        }
    }

    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[Authorize(PolicyMainApiNames.MainApi_Project_Write)]

    public async Task<ActionResult<Project>> PostProject(Project project)
    {
        using var mainDbContext = MainDbContextFactory.CreateDbContext();

        _ = mainDbContext.Projects.Add(project);

        var saveResult = await EFContext.InvokeAsync(() => mainDbContext.SaveChangesAsync());
        if (!saveResult.IsSuccess)
        { return ProblemFactory.ObjectResultFailedEFSave(saveResult); }

        return CreatedAtAction(
            nameof(PostProject),
            new { id = project.Id },
            project);
    }

    [HttpPut("{id:int}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[Authorize(PolicyMainApiNames.MainApi_Project_Write)]

    public async Task<IActionResult> PutProject(int id, Project project)
    {
        string message;

        if (id != project.Id)
        {
            message = $"id({id}) should be equal to project.Id({project.Id}).";
            return BadRequest(ProblemFactory.ObjectResultBadRequest(message));
        }

        using var mainDbContext = MainDbContextFactory.CreateDbContext();

        _ = mainDbContext.Attach(project);

        if (mainDbContext.Entry(project).GetDatabaseValues() is PropertyValues projectDbValues)
        { mainDbContext.Entry(project).OriginalValues.SetValues(projectDbValues); }
        else
        {
            message = $"Project with requested id({id}) wasn't found.";
            return ProblemFactory.ObjectResultNotFound(message);
        }


        var saveResult = await EFContext.InvokeAsync(() => mainDbContext.SaveChangesAsync());
        if (!saveResult.IsSuccess)
        { return ProblemFactory.ObjectResultFailedEFSave(saveResult); }

        return AcceptedAtAction(
            nameof(PutProject),
            new { id = project.Id },
            project);

    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[Authorize(PolicyMainApiNames.MainApi_Project_Write)]

    public async Task<IActionResult> DeleteProjectAsync(int id)
    {
        // Promijeniti u Async
        using var mainDbContext = MainDbContextFactory.CreateDbContext();

        Project project = new Project() { Id = id };
        mainDbContext.Projects.Attach(project);
        mainDbContext.Projects.Remove(project);

        var saveResult = await EFContext.InvokeAsync(() => mainDbContext.SaveChangesAsync());

        if (!saveResult.IsSuccess)
        { return ProblemFactory.ObjectResultFailedEFSave(saveResult); }

        return NoContent();
    }


    public void Dispose()
    {
        MainDbContext.Dispose();
    }
    private MainDbContext MainDbContext;

    private IDbContextFactory<MainDbContext> MainDbContextFactory { get; init; }

    private ASPProblemDetailsFactory ProblemFactory { get; init; }
}
