using ParkIstra.Services.EmailsSender;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData;
using NETCore.MailKit.Core;

var builder = WebApplication.CreateBuilder(args);
var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
#region EmailServer
builder.Services.AddEmailClientExtensions(builder.Configuration);
#endregion
_ = builder.Logging.ClearProviders();
_ = builder.Logging.AddConsole();
//_ = builder.Logging.AddFile();

var configuration = builder.Configuration;
var isDevelopment = builder.Environment.IsDevelopment();

_ = builder.Services.AddHstsService();
_ = builder.Services.AddHttpsRedirectionService(isDevelopment);
_ = builder.Services.AddASPProblemDetailsFactory(isDevelopment);
_ = builder.Services.AddEndpointsApiExplorer();
_ = builder.Services.AddSwaggerGen();

_ = builder.Services.AddControllers().AddODataService();
_ = builder.Services.AddRazorPages();

//_ = builder.Services.AddEndpointsApiExplorer();
//_ = builder.Services.AddSwaggerGen();
//_ = builder.Services.AddASPProblemDetailsFactory(isDevelopment);

#region DbContexts

var mainApiCorsPolicy = "mainApiCorsPolicy";
_ = builder.Services.AddCorsService(mainApiCorsPolicy);
_ = builder.Services.AddControllers().AddODataService();

_ = builder.Services.AddMainDbContext(isDevelopment, configuration);


//var utilApiCorsPolicy = "utilApiCorsPolicy";
//_ = builder.Services.AddCorsService(utilApiCorsPolicy);
//_ = builder.Services.AddControllers().AddODataService();

//_ = builder.Services.AddUtilDbContext(isDevelopment, configuration);

#endregion

#region Identity
builder.Services.AddIdentityExtensions();
#endregion

#region JWT
builder.Services.AddJWTExtensions(builder.Configuration, environmentName);
#endregion

#region Dependency
builder.Services.AddDependencyExtensions();
#endregion

#region policy
builder.Services.AddPolicyExtensions();
#endregion

//_ = builder.Services.AddSingleton<SqlScriptParser>();
//if (isDevelopment)
//{ /*_ = builder.Services.AddTransient<IPEndPoint, FunctionsEndpoints>();*/ }

//_ = builder.Services.AddJsonOptionsConfiguration();
//_ = builder.Services.Configure<SqlScriptsOptions>(
//    configuration.GetSection("ClubSqlScriptsOptions"));

var app = builder.Build();

var problemDetailsFactory = app.Services.GetRequiredService<ASPProblemDetailsFactory>();

_ = app.UseExceptionHandler(app => app.Run(async context =>
{
    var error = context.Features.Get<IExceptionHandlerFeature>()?.Error;
    error = error?.GetBaseException();

    var (statusCode, instance) = error is ODataException ?
        (StatusCodes.Status400BadRequest, "API_Handled_ODataException") :
        (StatusCodes.Status500InternalServerError, null);

    var problemDetails = problemDetailsFactory.CreateProblem(error, statusCode, instance);

    await context.Response.WriteAsJsonAsync(problemDetails);
}));

//if (builder.Environment.IsDevelopment() || builder.Environment.IsStaging()) // your choice
{
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI();
}

//_ = app.UseHttpsRedirection();

_ = app.UseStaticFiles();

if (isDevelopment)
{ _ = app.UseHttpsRedirection(); }
else
{ /* _ = app.UseHsts(); // not recommended in development */ }

_ = app.UseCors(mainApiCorsPolicy);

//foreach (var endpoint in app.Services.GetServices<IEndpoints>())
//{
//    ArgumentNullException.ThrowIfNull(endpoint, nameof(endpoint));

//    endpoint.Register(app);
//}
_ = app.UseAuthentication();
_ = app.UseAuthorization();

_ = app.MapControllers();

app.Run();
