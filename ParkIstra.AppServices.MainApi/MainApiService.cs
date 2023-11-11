namespace ParkIstra.AppServices.MainApi;

public partial class MainApiService : IMainApiService
{
    public MainApiService(IMainApiBroker mainApiBroker)
    {
        MainApiBroker = mainApiBroker;
    }

    private IMainApiBroker MainApiBroker { get; init; }
}
