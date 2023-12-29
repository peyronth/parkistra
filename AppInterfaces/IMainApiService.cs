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

    Task<Response<Testimonial>> GetTestimonialsAsync(ODataQuery? query = null);
    Task<Response<Testimonial>> GetTestimonialByIDAsync(int id, ODataQuery? query = null);
    Task<Response<Testimonial>> AddTestimonialAsync(Testimonial testimonial);
    Task<Response<Testimonial>> UpdateTestimonialAsync(int id, Testimonial testimonial);
    Task<Response<Testimonial>> DeleteTestimonialAsync(int id);

    #endregion

    #region Image
    Task<Response<Image>> GetImagesAsync(ODataQuery? query = null);
    Task<Response<Image>> GetImageByIDAsync(int id, ODataQuery? query = null);
    Task<Response<Image>> AddImageAsync(Image image);
    Task<Response<Image>> UpdateImageAsync(int id, Image image);
    Task<Response<Image>> DeleteImageAsync(int id);

    #endregion

    #region Authentication
    Task<Response<Response>> RegisterAsync(string Email, string Password, int UserType);
    Task<Response<Response>> LoginAsync(string Email, string Password);
    Task<Response<Response>> SendResetPwdLink(string email);
    Task<Response<Response>> ConfirmPwdLink(string email, string token, string newPassword);
    Task<Response<Response>> ConfirmEmail(string token, string email);
    Task<Response<ApplicationUser>> GetUserByEmail(string email);

    #endregion

}