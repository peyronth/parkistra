using ParkIstra.Libraries.Blazor;
using ParkIstra.Models.Main;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Components;
using ParkIstra.AppInterfaces;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace ParkIstra.AppBlazor.Client.Pages.Projects;
public partial class ProjectItem
{
    #region Lists
    [AllowNull]
    public List<Project> ProjectEntity { get; set; }
    private BlazorProblemDetails? BlazorProblemDetails { get; set; }

    #endregion
    protected override async Task OnInitializedAsync()
    {
        if (ProjectEntity is not null) return;

        await LoadProjectAsync();
    }

    private async Task LoadProjectAsync(string? queryString = "")
    {
        ODataQuery query = new()
        {
            ExpandList = new() { "ApplicationUser", "Images" }
        };

        var response = await MainApiService.GetProjectsAsync(query);
        BlazorProblemDetails = response.BlazorProblemDetails;
        if (response.IsSuccess)
        {
            ProjectEntity = response.Many ?? new();
        }
    }

    [Inject, AllowNull]
    private IMainApiService MainApiService { get; set; }
    [Inject, AllowNull]
    private IWebAssemblyHostEnvironment HostEnvironment { get; set; }
}
