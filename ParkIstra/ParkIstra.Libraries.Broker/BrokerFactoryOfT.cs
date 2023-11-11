namespace ParkIstra.Libraries.Blazor;
public class BrokerFactory
{
    public Broker<T> Create<T>(
        HttpClient httpClient,
        BlazorProblemDetailsFactory blazorProblemDetailsFactory) where T : class, new() =>
        new(httpClient, blazorProblemDetailsFactory);

    public CustomBroker<TResponse, T> Create<TResponse, T>(
        HttpClient httpClient,
        BlazorProblemDetailsFactory blazorProblemDetailsFactory) where T : class where TResponse : class, new() =>
        new(httpClient, blazorProblemDetailsFactory);
}
