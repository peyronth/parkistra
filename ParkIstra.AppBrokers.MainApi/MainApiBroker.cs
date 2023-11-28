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
        ApplicationUsersBroker = brokerFactory.Create<ApplicationUser>(httpClient, blazorProblemFactory);
        AuthenticationsBroker = brokerFactory.Create<Response>(httpClient, blazorProblemFactory);
        RegistersBroker = brokerFactory.Create<Response, ParkIstra.Models.Main.Register>(httpClient, blazorProblemFactory);
        LoginBroker = brokerFactory.Create<Response, ParkIstra.Models.Main.Login>(httpClient, blazorProblemFactory);
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

    #region Authentication
    public async Task<Response<Response>> RegisterAsync(string uri, ParkIstra.Models.Main.Register user) =>
        await RegistersBroker.PostAsync(uri, user, false);
    public async Task<Response<Response>> LoginAsync(string uri, ParkIstra.Models.Main.Login model) =>
       await LoginBroker.PostAsync(uri, model, false);
    public async Task<Response<Response>> SendResetPwdLink(string uri, bool isSingle = true) =>
        await AuthenticationsBroker.GetAsync(uri, isSingle);
    public async Task<Response<Response>> ConfirmPwdLink(string uri, bool isSingle = true) =>
        await AuthenticationsBroker.GetAsync(uri, isSingle);
    public async Task<Response<Response>> ConfirmEmail(string uri, bool isSingle = true) =>
        await AuthenticationsBroker.GetAsync(uri, isSingle);
    public async Task<Response<ApplicationUser>> GetUserByEmail(string uri, bool isSingle = true) =>
        await ApplicationUsersBroker.GetAsync(uri, isSingle);

    #endregion

    private Broker<Project> ProjectsBroker { get; init; }
    private Broker<Testimonials> TestimonialsBroker { get; init; }
    private Broker<Response> AuthenticationsBroker { get; init; }
    private CustomBroker<Response, ParkIstra.Models.Main.Register> RegistersBroker { get; init; }
    private CustomBroker<Response, ParkIstra.Models.Main.Login> LoginBroker { get; init; }
    private Broker<ApplicationUser> ApplicationUsersBroker { get; init; }

}