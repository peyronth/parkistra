using ParkIstra.Models.Util;

namespace ParkIstra.AppInterfaces;
public interface IUtilApiService
{
    #region SYS_Exception
    Task<Response<SYS_Exception>> GetSYS_ExceptionAsync(ODataQuery? query = null);
    Task<Response<SYS_Exception>> AddSYS_ExceptionAsync(SYS_Exception sys_exception);

    #endregion
}
