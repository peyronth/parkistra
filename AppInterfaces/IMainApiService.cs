namespace ParkIstra.AppInterfaces;

public interface IMainApiService
{
    #region Project
    Task<Response<Project>> GetProjectsAsync(ODataQuery? query = null);
    Task<Response<Project>> GetProjectByIDAsync(int id, ODataQuery? query = null);
    Task<Response<Project>> AddProjectAsync(Project project);
    Task<Response<Project>> UpdateProjectAsync(int id, Project project);
    Task<Response<Project>> DeleteProjectAsync(int id);

    #endregion

}