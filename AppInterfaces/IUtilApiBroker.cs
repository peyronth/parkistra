using ParkIstra.Models.Util;

namespace ParkIstra.AppInterfaces;

public interface IUtilApiBroker
{
    #region SYS_Exception
    Task<Response<SYS_Exception>> GetSYS_ExceptionAsync(string uri, bool isSingle = false);
    Task<Response<SYS_Exception>> PostSYS_ExceptionAsync(string uri, SYS_Exception sys_exception);

    #endregion
}
