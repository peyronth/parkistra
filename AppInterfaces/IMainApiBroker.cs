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
}