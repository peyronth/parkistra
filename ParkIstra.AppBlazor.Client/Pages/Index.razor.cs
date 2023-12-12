using ParkIstra.Libraries.Blazor;
using ParkIstra.Models.Main;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Components;
using ParkIstra.AppInterfaces;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace ParkIstra.AppBlazor.Client.Pages;
public partial class Index
{
    #region Lists
    [AllowNull]
    public List<Testimonial> Testimonials { get; set; }

    #endregion

    #region Properties
    private BlazorProblemDetails? BlazorProblemDetails { get; set; }
    private bool IsDevelopment { get; set; }

    private List<object> ToolbarList = new List<object>();
    private int? SelectedProjectId { get; set; } = null;
    private string SearchText { get; set; } = null!;
    private static bool EditModeBool { get; set; } = false;
    private ApplicationUser LoggedInUser { get; set; } = new();
    private int PageSize { get; set; } = 10;

    private Dictionary<string, object> btnAttr = new Dictionary<string, object>()
    {
        {"type","button" }
    };

    #endregion
    protected override async Task OnInitializedAsync()
    {
        if (Testimonials is not null) return;

        await LoadTestimonialsAsync();
    }

    private async Task LoadTestimonialsAsync(string? queryString = "")
    {
        ODataQuery query = new()
        {

        };

        var response = await MainApiService.GetTestimonialsAsync(query);
        BlazorProblemDetails = response.BlazorProblemDetails;
        if (response.IsSuccess)
        {
            Testimonials = response.Many ?? new();
        }

    }

    [Inject, AllowNull]
    private IMainApiService MainApiService { get; set; }
    [Inject, AllowNull]
    private IWebAssemblyHostEnvironment HostEnvironment { get; set; }
}
