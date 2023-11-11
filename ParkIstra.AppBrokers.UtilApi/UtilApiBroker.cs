using ParkIstra.Models.Util;

namespace ParkIstra.AppBrokers.UtilApi;
public partial class UtilApiBroker : IUtilApiBroker
{
    public UtilApiBroker(
        HttpClient httpClient,
        BlazorProblemDetailsFactory blazorProblemFactory,
        BrokerFactory brokerFactory)
    {
        SYS_ExceptionBroker = brokerFactory.Create<SYS_Exception>(httpClient, blazorProblemFactory);

    }

    #region SYS_Exception
    public async Task<Response<SYS_Exception>> GetSYS_ExceptionAsync(string uri, bool isSingle = false) =>
        await SYS_ExceptionBroker.GetAsync(uri, isSingle);

    public async Task<Response<SYS_Exception>> PostSYS_ExceptionAsync(string uri, SYS_Exception sys_exception) =>
        await SYS_ExceptionBroker.PostAsync(uri, sys_exception);

    #endregion



    private Broker<SYS_Exception> SYS_ExceptionBroker { get; init; }



}
