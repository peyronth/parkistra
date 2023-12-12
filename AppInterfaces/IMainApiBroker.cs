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
    Task<Response<Testimonial>> GetTestimonialsAsync(string uri, bool isSingle = false);
    Task<Response<Testimonial>> GetTestimonialByIDAsync(string uri, bool isSingle = true);
    Task<Response<Testimonial>> PostTestimonialAsync(string uri, Testimonial testimonial);
    Task<Response<Testimonial>> PutTestimonialAsync(string uri, Testimonial testimonial);
    Task<Response<Testimonial>> DeleteTestimonialAsync(string uri);

    #endregion

    #region Image
    Task<Response<Image>> GetImagesAsync(string uri, bool isSingle = false);
    Task<Response<Image>> GetImageByIDAsync(string uri, bool isSingle = true);
    Task<Response<Image>> PostImageAsync(string uri, Image image);
    Task<Response<Image>> PutImageAsync(string uri, Image image);
    Task<Response<Image>> DeleteImageAsync(string uri);

    #endregion
}