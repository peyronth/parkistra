namespace ParkIstra.AppServices.UtilApi;

public partial class UtilApiService : IUtilApiService
{
    public UtilApiService(IUtilApiBroker utilApiBroker)
    {
        UtilApiBroker = utilApiBroker;
    }

    private IUtilApiBroker UtilApiBroker { get; init; }
}
