namespace ParkIstra.AppServices.MainApi;

public partial class MainApiService
{
    public async Task<Response<Project>> GetProjectsAsync(ODataQuery? query = null) =>
        await MainApiBroker.GetProjectsAsync($"Project{query}");
    public async Task<Response<Project>> GetProjectByIDAsync(int id, ODataQuery? query = null) =>
       await MainApiBroker.GetProjectByIDAsync($"Project/{id}{query}");
    public async Task<Response<Project>> AddProjectAsync(Project project) =>
        await MainApiBroker.PostProjectAsync("Project", GetPreparedProject(project));
    public async Task<Response<Project>> UpdateProjectAsync(int id, Project project) =>
        await MainApiBroker.PutProjectAsync($"Project/{id}", GetPreparedProject(project));

    public async Task<Response<Project>> DeleteProjectAsync(int id) =>
        await MainApiBroker.DeleteProjectAsync($"Project/{id}");

    private static Project GetPreparedProject(Project project)
    {
        var preparedProject = JsonSerializer.Deserialize<Project>(
            JsonSerializer.Serialize(project))!;

        return preparedProject;
    }

}
