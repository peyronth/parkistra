using System.Diagnostics.CodeAnalysis;
using ParkIstra.Libraries.Blazor;
using ParkIstra.AppInterfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using ParkIstra.Models.Main;

namespace ParkIstra.AppBlazor.Client.Pages.Member.Login;
public partial class LoginPage
{
    #region Properties

    [AllowNull]
    private ParkIstra.Models.Main.Login LoginModel { get; set; } = new();
    private BlazorProblemDetails? BlazorProblemDetails { get; set; }

    #endregion

    protected override async Task OnInitializedAsync()
    {
    }

    private async Task Submit()
    {
        var response = await MainApiService.LoginAsync(LoginModel);
        BlazorProblemDetails = response.BlazorProblemDetails;
        if (BlazorProblemDetails != null || response.Single.Status == false)
        {
            if (response.Single != null && response.Single.Message.Equals("Confirm mail"))
            {
                await ErrorHandler.HandleRequestError("Greška pri logovanju. Molimo Vas potvrdite mail.");
            }
            else if (BlazorProblemDetails != null)
            {
                await ErrorHandler.HandleRequestError("Pogrešni podaci.");
            }
            return;
        }
        if (response.IsSuccess) { 
      
            await jsr.InvokeVoidAsync("localStorage.setItem", "jwt", $"{response.Single.token}").ConfigureAwait(false);
            NavigationManager.NavigateTo($"/samoprocjena?email={LoginModel.Email}");
        }
    }
    private async void ForgotPassword()
    {
        if (LoginModel.Email != null)
        {
            var response = await MainApiService.SendResetPwdLink(LoginModel.Email);
            BlazorProblemDetails = response.BlazorProblemDetails;
            if (BlazorProblemDetails != null || response.Single.Status == false)
            {
                if (response.Single != null && response.Single.Message.Equals("Invalid email"))
                {
                    await ErrorHandler.HandleRequestError("Nepostojeći email.");
                }
                return;
            }
        }
        else
        {
            await ErrorHandler.HandleRequestError("Molimo unesite email za izmjenu lozinke.");
        }
    }
    private void Register()
    {
        NavigationManager.NavigateTo("/register");
    }

    [Inject, AllowNull]
    private IMainApiService MainApiService { get; set; }

    [Inject, AllowNull]
    private ErrorHandler ErrorHandler { get; set; }
}