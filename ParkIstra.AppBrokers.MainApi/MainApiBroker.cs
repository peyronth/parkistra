namespace ParkIstra.AppBrokers.MainApi;
public partial class MainApiBroker : IMainApiBroker
{
    public MainApiBroker(
        HttpClient httpClient,
        BlazorProblemDetailsFactory blazorProblemFactory,
        BrokerFactory brokerFactory)
    {
        ProjectsBroker = brokerFactory.Create<Project>(httpClient, blazorProblemFactory);
        TestimonialsBroker = brokerFactory.Create<Testimonial>(httpClient, blazorProblemFactory);
        ImagesBroker = brokerFactory.Create<Image>(httpClient, blazorProblemFactory);
    }

    #region Project
    public async Task<Response<Project>> GetProjectsAsync(string uri, bool isSingle = false) =>
        await ProjectsBroker.GetAsync(uri, isSingle);
    public async Task<Response<Project>> GetProjectByIDAsync(string uri, bool isSingle = true) =>
       await ProjectsBroker.GetAsync(uri, isSingle);
    public async Task<Response<Project>> PostProjectAsync(string uri, Project project) =>
        await ProjectsBroker.PostAsync(uri, project);
    public async Task<Response<Project>> PutProjectAsync(string uri, Project project) =>
        await ProjectsBroker.PutAsync(uri, project);
    public async Task<Response<Project>> DeleteProjectAsync(string uri) =>
        await ProjectsBroker.DeleteAsync(uri);

    #endregion

    #region Testimonials
    public async Task<Response<Testimonial>> GetTestimonialsAsync(string uri, bool isSingle = false) =>
        await TestimonialsBroker.GetAsync(uri, isSingle);
    public async Task<Response<Testimonial>> GetTestimonialByIDAsync(string uri, bool isSingle = true) =>
         await TestimonialsBroker.GetAsync(uri, isSingle);
    public async Task<Response<Testimonial>> PostTestimonialAsync(string uri, Testimonial testimonial) =>
        await TestimonialsBroker.PostAsync(uri, testimonial);
    public async Task<Response<Testimonial>> PutTestimonialAsync(string uri, Testimonial testimonial) =>
        await TestimonialsBroker.PutAsync(uri, testimonial);
    public async Task<Response<Testimonial>> DeleteTestimonialAsync(string uri) =>
        await TestimonialsBroker.DeleteAsync(uri);

    #endregion
    
    #region Image
    public async Task<Response<Image>> GetImagesAsync(string uri, bool isSingle = false) =>
        await ImagesBroker.GetAsync(uri, isSingle);
    public async Task<Response<Image>> GetImageByIDAsync(string uri, bool isSingle = true) =>
         await ImagesBroker.GetAsync(uri, isSingle);
    public async Task<Response<Image>> PostImageAsync(string uri, Image image) =>
        await ImagesBroker.PostAsync(uri, image);
    public async Task<Response<Image>> PutImageAsync(string uri, Image image) =>
        await ImagesBroker.PutAsync(uri, image);
    public async Task<Response<Image>> DeleteImageAsync(string uri) =>
        await ImagesBroker.DeleteAsync(uri);

    #endregion

    private Broker<Project> ProjectsBroker { get; init; }
    private Broker<Testimonial> TestimonialsBroker { get; init; }
    private Broker<Image> ImagesBroker { get; init; }

}