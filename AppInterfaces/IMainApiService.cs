namespace ParkIstra.AppInterfaces;

public interface IMainApiService
{
    #region Project
    Task<Response<Project>> GetProjectsAsync(ODataQuery? query = null);
    Task<Response<Project>> GetProjectByIDAsync(int id, ODataQuery? query = null);
    Task<Response<Project>> AddProjectAsync(Project project);
    Task<Response<Project>> UpdateProjectAsync(int id, Project project);
    Task<Response<Project>> DeleteProjectAsync(int id);

    #endregion

    #region Testimonials

    Task<Response<Testimonials>> GetTestimonialsAsync(ODataQuery? query = null);
    Task<Response<Testimonials>> GetTestimonialByIDAsync(int id, ODataQuery? query = null);
    Task<Response<Testimonials>> AddTestimonialAsync(Testimonials testimonial);
    Task<Response<Testimonials>> UpdateTestimonialAsync(int id, Testimonials testimonial);
    Task<Response<Testimonials>> DeleteTestimonialAsync(int id);

    #endregion

    #region Authentiacion
    Task<Response<Response>> RegisterAsync(ParkIstra.Models.Main.Register user);
    Task<Response<Response>> LoginAsync(ParkIstra.Models.Main.Login model);
    Task<Response<Response>> SendResetPwdLink(string email);
    Task<Response<Response>> ConfirmPwdLink(string email, string token, string newPassword);
    Task<Response<Response>> ConfirmEmail(string token, string email);
    Task<Response<ApplicationUser>> GetUserByEmail(string email);

    #endregion

}