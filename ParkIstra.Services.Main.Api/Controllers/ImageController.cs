namespace ParkIstra.Services.MainApi.Controllers;

[Route("[controller]")]
[ApiController]
//[Authorize]
public class ImageController : ControllerBase, IDisposable
{

    public ImageController(
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
    //[Authorize(PolicyMainApiNames.MainApi_Image_Read)]

    public async Task<ActionResult<IQueryable<Image>>> GetAllImages()
    {
        var query = MainDbContext.Images;

        return Ok(query);
    }


    [HttpGet("{id:int}")]
    [OdataEnableQuery]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    //[Authorize(PolicyMainApiNames.MainApi_Image_Read)]

    public async Task<ActionResult<Image>> GetImageById(int id)
    {
        var query = SingleResult.Create(
            MainDbContext.Images.Where(m => m.Id == id));

        return Ok(query);
    }

    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[Authorize(PolicyMainApiNames.MainApi_Image_Write)]

    public async Task<ActionResult<Image>> PostImage(Image image)
    {
        using var mainDbContext = MainDbContextFactory.CreateDbContext();

        _ = mainDbContext.Images.Add(image);

        var saveResult = await EFContext.InvokeAsync(() => mainDbContext.SaveChangesAsync());
        if (!saveResult.IsSuccess)
        { return ProblemFactory.ObjectResultFailedEFSave(saveResult); }

        return CreatedAtAction(
            nameof(PostImage),
            new { id = image.Id },
            image);
    }

    [HttpPut("{id:int}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[Authorize(PolicyMainApiNames.MainApi_Image_Write)]

    public async Task<IActionResult> PutImage(int id, Image image)
    {
        string message;

        if (id != image.Id)
        {
            message = $"id({id}) should be equal to Image.Id({image.Id}).";
            return BadRequest(ProblemFactory.ObjectResultBadRequest(message));
        }

        using var mainDbContext = MainDbContextFactory.CreateDbContext();

        _ = mainDbContext.Attach(image);

        if (mainDbContext.Entry(image).GetDatabaseValues() is PropertyValues imageDbValues)
        { mainDbContext.Entry(image).OriginalValues.SetValues(imageDbValues); }
        else
        {
            message = $"Image with requested id({id}) wasn't found.";
            return ProblemFactory.ObjectResultNotFound(message);
        }


        var saveResult = await EFContext.InvokeAsync(() => mainDbContext.SaveChangesAsync());
        if (!saveResult.IsSuccess)
        { return ProblemFactory.ObjectResultFailedEFSave(saveResult); }

        return AcceptedAtAction(
            nameof(PutImage),
            new { id = image.Id },
            image);

    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[Authorize(PolicyMainApiNames.MainApi_Image_Write)]

    public async Task<IActionResult> DeleteImageAsync(int id)
    {
        // Promijeniti u Async
        using var mainDbContext = MainDbContextFactory.CreateDbContext();

        Image Image = new Image() { Id = id };
        mainDbContext.Images.Attach(Image);
        mainDbContext.Images.Remove(Image);

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
