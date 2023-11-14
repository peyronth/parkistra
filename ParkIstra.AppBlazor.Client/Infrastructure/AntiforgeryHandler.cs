using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.JSInterop;
using System.Net.Http.Headers;

namespace ParkIstra.AppBlazor.Client.Infrastructure;

public class CustomHttpMessageHandler : DelegatingHandler
{
    private readonly IJSRuntime _js;
    public CustomHttpMessageHandler(IJSRuntime js)
    {
        _js = js;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        //var afrt = await _js.InvokeAsync<string>("getCookie", ".AFRT");
        string val = await _js.InvokeAsync<string>("localStorage.getItem", "jwt");
        request.Headers.Add("Authorization", "Bearer " + val);

        return await base.SendAsync(request, cancellationToken);
    }
}
