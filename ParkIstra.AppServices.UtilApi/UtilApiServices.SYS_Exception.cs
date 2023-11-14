using ParkIstra.Models.Util;

namespace ParkIstra.AppServices.UtilApi;

public partial class UtilApiService
{
    public async Task<Response<SYS_Exception>> GetSYS_ExceptionAsync(ODataQuery? query = null) =>
        await UtilApiBroker.GetSYS_ExceptionAsync($"SYS_Exception{query}");

    public async Task<Response<SYS_Exception>> AddSYS_ExceptionAsync(SYS_Exception sys_exception) =>
        await UtilApiBroker.PostSYS_ExceptionAsync("SYS_Exception", GetPreparedSYS_Exception(sys_exception));

    private static SYS_Exception GetPreparedSYS_Exception(SYS_Exception sys_exception)
    {
        var preparedSYS_Exception = JsonSerializer.Deserialize<SYS_Exception>(
            JsonSerializer.Serialize(sys_exception))!; 

        return preparedSYS_Exception;
    }
  
}

