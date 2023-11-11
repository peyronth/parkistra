using ParkIstra.Libraries.Blazor;
using System.Threading.Tasks;

namespace ParkIstra.Libraries.Blazor;

public class CustomBroker<TResponse, T> where T : class where TResponse : class
{
    public CustomBroker(
        HttpClient httpClient,
        BlazorProblemDetailsFactory blazorProblemDetailsFactory)
    {
        ProblemFactory = blazorProblemDetailsFactory;
        HttpClient = httpClient;
    }

    public async Task<Response<T>> GetAsync(string uri, bool isSingle = false)
    {
        var response = new Response<T>();

        try
        {
            var payload = await HttpClient.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead);
            response.IsSuccess = payload.IsSuccessStatusCode;
            //response.Status = payload.StatusCode; // don't need this

            if (response.IsSuccess)
            {
                if (!isSingle)
                { response.Many = await payload.Content.ReadFromJsonAsync<List<T>>(); }
                else
                { response.Single = await payload.Content.ReadFromJsonAsync<T>(); }
            }
            else
            { response.BlazorProblemDetails = await payload.Content.ReadFromJsonAsync<BlazorProblemDetails>(); }
        }
        catch (Exception exception)
        {
            response.IsSuccess = false;
            response.BlazorProblemDetails = ProblemFactory.CreateBlazorProblemDetails(exception);
        }

        return response;
    }

    public async Task<Response<TResponse>> PostAsync(string uri, T entity, bool many = true)
    {
        var response = new Response<TResponse>();

        try
        {
            var payload = await HttpClient.PostAsJsonAsync(uri, entity);
            response.IsSuccess = payload.IsSuccessStatusCode;

            if (response.IsSuccess)
            {
                if (many == true)
                {
                    response.Many = await payload.Content.ReadFromJsonAsync<List<TResponse>>();
                }
                else
                {
                    response.Single = await payload.Content.ReadFromJsonAsync<TResponse>();
                }
            }

            else
            { response.BlazorProblemDetails = await payload.Content.ReadFromJsonAsync<BlazorProblemDetails>(); }
        }
        catch (Exception exception)
        {
            response.IsSuccess = false;
            response.BlazorProblemDetails = ProblemFactory.CreateBlazorProblemDetails(exception);
        }

        return response;
    }

    public async Task<Response<TResponse>> PostAsyncWrapper(string uri, T entity)
    {
        var response = new Response<TResponse>();

        try
        {
            var payload = await HttpClient.PostAsJsonAsync(uri, entity);
            response.IsSuccess = payload.IsSuccessStatusCode;

            if (response.IsSuccess)
            {
                response.Single = await payload.Content.ReadFromJsonAsync<TResponse>();
            }

            else
            { response.BlazorProblemDetails = await payload.Content.ReadFromJsonAsync<BlazorProblemDetails>(); }
        }
        catch (Exception exception)
        {
            response.IsSuccess = false;
            response.BlazorProblemDetails = ProblemFactory.CreateBlazorProblemDetails(exception);
        }

        return response;
    }

    public async Task<Response<T>> PutAsync(string uri, T entity)
    {
        var response = new Response<T>();

        try
        {
            var payload = await HttpClient.PutAsJsonAsync(uri, entity);
            response.IsSuccess = payload.IsSuccessStatusCode;

            if (response.IsSuccess)
            { response.Single = entity; } // = await payload.Content.ReadFromJsonAsync<T>(); }
            else
            { response.BlazorProblemDetails = await payload.Content.ReadFromJsonAsync<BlazorProblemDetails>(); }
        }
        catch (Exception exception)
        {
            response.IsSuccess = false;
            response.BlazorProblemDetails = ProblemFactory.CreateBlazorProblemDetails(exception);
        }

        return response;
    }

    public async Task<Response<T>> DeleteAsync(string uri)
    {
        var response = new Response<T>();

        try
        {
            var payload = await HttpClient.DeleteAsync(uri);
            response.IsSuccess = payload.IsSuccessStatusCode;

            if (!response.IsSuccess)
            { response.BlazorProblemDetails = await payload.Content.ReadFromJsonAsync<BlazorProblemDetails>(); }
        }
        catch (Exception exception)
        {
            response.IsSuccess = false;
            response.BlazorProblemDetails = ProblemFactory.CreateBlazorProblemDetails(exception);
        }

        return response;
    }

    private HttpClient HttpClient { get; init; }
    private BlazorProblemDetailsFactory ProblemFactory { get; init; }
}
