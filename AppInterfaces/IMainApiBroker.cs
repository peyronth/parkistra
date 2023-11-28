namespace ParkIstra.AppInterfaces;

public interface IMainApiBroker
{
    #region Project
    Task<Response<Project>> GetProjectsAsync(string uri, bool isSingle = false);
    Task<Response<Project>> GetProjectByIDAsync(string uri, bool isSingle = true);
    Task<Response<Project>> PostProjectAsync(string uri, Project project);
    Task<Response<Project>> PutProjectAsync(string uri, Project project);
    Task<Response<Project>> DeleteProjectAsync(string uri);

    #endregion

    #region Testimonials
    Task<Response<Testimonials>> GetTestimonialsAsync(string uri, bool isSingle = false);
    Task<Response<Testimonials>> GetTestimonialByIDAsync(string uri, bool isSingle = true);
    Task<Response<Testimonials>> PostTestimonialAsync(string uri, Testimonials testimonial);
    Task<Response<Testimonials>> PutTestimonialAsync(string uri, Testimonials testimonial);
    Task<Response<Testimonials>> DeleteTestimonialAsync(string uri);

    #endregion

    #region Authentiacion
    Task<Response<Response>> RegisterAsync(string uri, ParkIstra.Models.Main.Register user);
    Task<Response<Response>> LoginAsync(string uri, ParkIstra.Models.Main.Login model);
    Task<Response<Response>> SendResetPwdLink(string uri, bool isSingle = true);
    Task<Response<Response>> ConfirmPwdLink(string uri, bool isSingle = true);
    Task<Response<Response>> ConfirmEmail(string uri, bool isSingle = true);
    Task<Response<ApplicationUser>> GetUserByEmail(string uri, bool isSingle = true);

    #endregion
}