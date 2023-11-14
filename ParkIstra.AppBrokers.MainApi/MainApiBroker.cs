namespace ParkIstra.AppBrokers.MainApi;
public partial class MainApiBroker : IMainApiBroker
{
    public MainApiBroker(
        HttpClient httpClient,
        BlazorProblemDetailsFactory blazorProblemFactory,
        BrokerFactory brokerFactory)
    {
        ProjectsBroker = brokerFactory.Create<Project>(httpClient, blazorProblemFactory);
        TestimonialsBroker = brokerFactory.Create<Testimonials>(httpClient, blazorProblemFactory);
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
    public async Task<Response<Testimonials>> GetTestimonialsAsync(string uri, bool isSingle = false) =>
        await TestimonialsBroker.GetAsync(uri, isSingle);
    public async Task<Response<Testimonials>> GetTestimonialByIDAsync(string uri, bool isSingle = true) =>
         await TestimonialsBroker.GetAsync(uri, isSingle);
    public async Task<Response<Testimonials>> PostTestimonialAsync(string uri, Testimonials testimonial) =>
        await TestimonialsBroker.PostAsync(uri, testimonial);
    public async Task<Response<Testimonials>> PutTestimonialAsync(string uri, Testimonials testimonial) =>
        await TestimonialsBroker.PutAsync(uri, testimonial);
    public async Task<Response<Testimonials>> DeleteTestimonialAsync(string uri) =>
        await TestimonialsBroker.DeleteAsync(uri);

    #endregion

    private Broker<Project> ProjectsBroker { get; init; }
    private Broker<Testimonials> TestimonialsBroker { get; init; }

}