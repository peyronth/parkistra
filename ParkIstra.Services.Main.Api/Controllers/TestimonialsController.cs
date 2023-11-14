using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ParkIstra.Services.MainApi.Controllers;

[Route("[controller]")]
[ApiController]
//[Authorize]
public class TestimonialsController : ControllerBase, IDisposable
{

    public TestimonialsController(
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
    //[Authorize(PolicyMainApiNames.MainApi_Testimonials_Read)]

    public async Task<ActionResult<IQueryable<Testimonials>>> GetAllTestimonials(int? numberOfArt = null)
    {
        //if (numberOfArt != null)
        //{
        //    var query = MainDbContext.Testimonials.;

        //    return Ok(query);
        //}
        //else
        //{
            var query = MainDbContext.Testimonials;

            return Ok(query);
        //}
    }

    [HttpGet("BySortOrBySearch")]
    [OdataEnableQuery]
    //[Authorize(PolicyMainApiNames.MainApi_Testimonials_Read)]

    public async Task<ActionResult<IQueryable<Testimonials>>> GetAllTestimonialsBySearchOrSort(int numberOfArt)
    {
        var query = MainDbContext.Testimonials;

        return Ok(query);
    }

    [HttpGet("{id:int}")]
    [OdataEnableQuery]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    //[Authorize(PolicyMainApiNames.MainApi_Testimonials_Read)]

    public async Task<ActionResult<Testimonials>> GetTestimonialById(int id)
    {
        var query = SingleResult.Create(
            MainDbContext.Testimonials.Where(m => m.Id == id));

        return Ok(query);
    }

    [HttpGet("count")]
    //[Authorize(PolicyMainApiNames.MainApi_Testimonials_Read)]

    public ActionResult<IntWrapper> GetTestimonialsCount(string? search)
    {
        if (search == null)
        {
            return Ok(new IntWrapper(MainDbContext.Testimonials.Count()));

        }
        else
        {
            return Ok(new IntWrapper(MainDbContext.Testimonials.Where(x => x.Content.Contains(search)).Count()));
        }
    }

    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[Authorize(PolicyMainApiNames.MainApi_Testimonials_Write)]

    public async Task<ActionResult<Testimonials>> PostTestimonial(Testimonials Testimonials)
    {
        using var mainDbContext = MainDbContextFactory.CreateDbContext();

        _ = mainDbContext.Testimonials.Add(Testimonials);

        var saveResult = await EFContext.InvokeAsync(() => mainDbContext.SaveChangesAsync());
        if (!saveResult.IsSuccess)
        { return ProblemFactory.ObjectResultFailedEFSave(saveResult); }

        return CreatedAtAction(
            nameof(PostTestimonial),
            new { id = Testimonials.Id },
            Testimonials);
    }

    [HttpPut("{id:int}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[Authorize(PolicyMainApiNames.MainApi_Testimonials_Write)]

    public async Task<IActionResult> PutTestimonial(int id, Testimonials Testimonials)
    {
        string message;

        if (id != Testimonials.Id)
        {
            message = $"id({id}) should be equal to Testimonials.Id({Testimonials.Id}).";
            return BadRequest(ProblemFactory.ObjectResultBadRequest(message));
        }

        using var mainDbContext = MainDbContextFactory.CreateDbContext();

        _ = mainDbContext.Attach(Testimonials);

        if (mainDbContext.Entry(Testimonials).GetDatabaseValues() is PropertyValues TestimonialsDbValues)
        { mainDbContext.Entry(Testimonials).OriginalValues.SetValues(TestimonialsDbValues); }
        else
        {
            message = $"Testimonials with requested id({id}) wasn't found.";
            return ProblemFactory.ObjectResultNotFound(message);
        }


        var saveResult = await EFContext.InvokeAsync(() => mainDbContext.SaveChangesAsync());
        if (!saveResult.IsSuccess)
        { return ProblemFactory.ObjectResultFailedEFSave(saveResult); }

        return AcceptedAtAction(
            nameof(PutTestimonial),
            new { id = Testimonials.Id },
            Testimonials);

    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[Authorize(PolicyMainApiNames.MainApi_Testimonials_Write)]

    public async Task<IActionResult> DeleteTestimonialAsync(int id)
    {
        // Promijeniti u Async
        using var mainDbContext = MainDbContextFactory.CreateDbContext();

        Testimonials Testimonials = new Testimonials() { Id = id };
        mainDbContext.Testimonials.Attach(Testimonials);
        mainDbContext.Testimonials.Remove(Testimonials);

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
