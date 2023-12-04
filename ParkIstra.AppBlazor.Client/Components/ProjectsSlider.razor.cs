using ParkIstra.Libraries.Blazor;
using ParkIstra.Models.Main;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Components;
using ParkIstra.AppInterfaces;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace ParkIstra.AppBlazor.Client.Components;
public partial class ProjectsSlider
{
    #region Lists
    [AllowNull]
    public List<Project> Projects { get; set; }

    #endregion
    protected override async Task OnInitializedAsync()
    {
        if (Projects is not null) return;

        await LoadProjectsAsync();
    }

    private async Task LoadProjectsAsync(string? queryString = "")
    {
        
        //Mock data
        Projects = new List<Project>
        {
            new Project { Id = 122, Name = "Project 1", Description = "Description 1", Images = new List<string> { "http://parkistra.com/en/wp-content/uploads/2017/06/20170218_094211.jpg" } },
            new Project { Id = 132, Name = "Project 2", Description = "Description 2", Images = new List<string> { "http://parkistra.com/en/wp-content/uploads/2018/04/20170703_093011.jpg" } },
            new Project { Id = 143, Name = "Project 3", Description = "Description 3", Images = new List<string> { "http://parkistra.com/en/wp-content/uploads/2017/06/20170218_094211.jpg" } },
            new Project { Id = 154, Name = "Project 4", Description = "Description 4", Images = new List<string> { "http://parkistra.com/en/wp-content/uploads/2018/04/20170703_093011.jpg" } },
            new Project { Id = 165, Name = "Project 5", Description = "Description 5", Images = new List<string> { "http://parkistra.com/en/wp-content/uploads/2017/06/20170218_094211.jpg" } },
            new Project { Id = 176, Name = "Project 6", Description = "Description 6", Images = new List<string> { "http://parkistra.com/en/wp-content/uploads/2018/04/20170703_093011.jpg" } },
            new Project { Id = 187, Name = "Project 7", Description = "Description 7", Images = new List<string> { "http://parkistra.com/en/wp-content/uploads/2017/06/20170218_094211.jpg" } },
            new Project { Id = 198, Name = "Project 8", Description = "Description 8", Images = new List<string> { "http://parkistra.com/en/wp-content/uploads/2018/04/20170703_093011.jpg" } }
        };

    }

    [Inject, AllowNull]
    private IMainApiService MainApiService { get; set; }
    [Inject, AllowNull]
    private IWebAssemblyHostEnvironment HostEnvironment { get; set; }
}
