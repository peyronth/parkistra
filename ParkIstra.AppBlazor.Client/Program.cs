using ParkIstra.AppBlazor.Client;
using ParkIstra.AppBlazor.Client.Extensions;
using ParkIstra.AppBlazor.Client.Infrastructure;
using ParkIstra.AppBrokers.UtilApi;
using ParkIstra.AppBrokers.MainApi;
using ParkIstra.AppInterfaces;
using ParkIstra.AppServices.UtilApi;
using ParkIstra.AppServices.MainApi;
using ParkIstra.Libraries.Blazor;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using System.Globalization;
using System.Net;
using AKSoftware.Localization.MultiLanguages;
using System.Reflection;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddLanguageContainer(Assembly.GetExecutingAssembly());


var httpClient = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
var isDevelopment = builder.HostEnvironment.IsDevelopment();


_ = builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

_ = builder.Services.AddSingleton<IStateContainer, StateContainer>();
_ = builder.Services.AddScoped<ErrorHandler>(); //testservice

#region unused
_ = builder.Services.AddScoped<IMainApiService, MainApiService>();
_ = builder.Services.AddHttpClient<IMainApiBroker, MainApiBroker>(client =>
    client.BaseAddress = new Uri(builder.Configuration["MainApiUri"] ?? "")).AddHttpMessageHandler<CustomHttpMessageHandler>();

_ = builder.Services.AddScoped<IUtilApiService, UtilApiService>();
_ = builder.Services.AddHttpClient<IUtilApiBroker, UtilApiBroker>(client =>
    client.BaseAddress = new Uri(builder.Configuration["MainApiUri"] ?? "")).AddHttpMessageHandler<CustomHttpMessageHandler>();
#endregion

//Identity - authorize view - bff
builder.Services.AddScoped<CustomHttpMessageHandler>();
builder.Services.AddHttpClient("ApiClient", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<CustomHttpMessageHandler>();
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("ApiClient"));



_ = builder.Services.AddSingleton<IStateContainer, StateContainer>();

_ = builder.Services.AddBlazorProblemDetailsFactory(isDevelopment);
_ = builder.Services.AddSingleton<BrokerFactory>();

_ = builder.Services.AddJsonSerializerOptions();
_ = await builder.Configuration.AddConfigurationsAsync(httpClient);



var host = builder.Build();

await host.RunAsync();