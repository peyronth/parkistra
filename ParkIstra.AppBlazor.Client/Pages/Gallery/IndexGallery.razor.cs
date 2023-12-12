using ParkIstra.Models.Main;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Components;
using ParkIstra.AppInterfaces;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace ParkIstra.AppBlazor.Client.Pages.Gallery;
public partial class IndexGallery
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

    private async Task LoadProjectsAsync()
    {
        
        //TODO : remove mock
        Projects = new List<Project>
        {
            new Project { Id = 122, Name = "Project 1", Description = "Description 1", Images = new List<string> { "http://parkistra.com/en/wp-content/uploads/2017/06/20170218_094211.jpg" } },
            new Project { Id = 132, Name = "Project 2", Description = "Description 2", Images = new List<string> { "http://parkistra.com/en/wp-content/uploads/2018/04/20170703_093011.jpg" } },
            new Project { Id = 143, Name = "Project 3", Description = "Description 3", Images = new List<string> { "http://parkistra.com/en/wp-content/uploads/2017/06/20170218_094211.jpg", "http://parkistra.com/en/wp-content/uploads/2018/04/20170703_093011.jpg" } },
            new Project { Id = 154, Name = "Project 4", Description = "Description 4", Images = new List<string> { "http://parkistra.com/en/wp-content/uploads/2018/04/20170703_093011.jpg" } }
        };

    }

    [Inject, AllowNull]
    private IMainApiService MainApiService { get; set; }
    [Inject, AllowNull]
    private IWebAssemblyHostEnvironment HostEnvironment { get; set; }
}
