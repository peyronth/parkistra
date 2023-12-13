using Microsoft.AspNetCore.Components;
using ParkIstra.AppInterfaces;
using ParkIstra.Libraries.Blazor;
using System.Diagnostics.CodeAnalysis;
using System.Web;

namespace ParkIstra.AppBlazor.Client.Pages.Member.Login;
public partial class ChangePasswordPage
{
    #region Properties
    private BlazorProblemDetails? BlazorProblemDetails { get; set; }
    private string? Password { get; set; }
    private string? ConfirmPassword { get; set; }
    private bool isSuccess { get; set; }
    [Parameter]
    [SupplyParameterFromQuery(Name = "email")]
    public string email { get; set; }
    [Parameter]
    [SupplyParameterFromQuery(Name = "token")]
    public string code { get; set; }

    #endregion
    protected override async Task OnInitializedAsync()
    {
    }
    public async Task ChangePassword()
    {
        if (Password != null || ConfirmPassword != null)
        {
            if (Password.Equals(ConfirmPassword))
            {
                var temp = HttpUtility.UrlEncode(code);
                var response = await MainApiService.ConfirmPwdLink(email, temp, Password);
                BlazorProblemDetails = response.BlazorProblemDetails;
                if (BlazorProblemDetails != null || response.Single.Status == false)
                {
                    if (response.Single.Message.Equals("Invalid Request"))
                    {
                        await ErrorHandler.HandleRequestError("Nevalidan token.");
                    }
                    return;
                }
                isSuccess = true;
                this.StateHasChanged();
            }
            else
            {
                await ErrorHandler.HandleRequestError("Lozinka i lozinka potvrde se ne poklapaju.");
            }
        }
        else
        {
            await ErrorHandler.HandleRequestError("Morate unijeti i lozinku i potvrdu lozinke.");
        }

    }
    private void GoBack()
    {
        NavigationManager.NavigateTo("/");
    }

    [Inject, AllowNull]
    private IMainApiService MainApiService { get; set; }
    [Inject, AllowNull]
    private ErrorHandler ErrorHandler { get; set; }
}