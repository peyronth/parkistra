using System.Diagnostics.CodeAnalysis;
using ParkIstra.Libraries.Blazor;
using ParkIstra.AppInterfaces;
using Microsoft.AspNetCore.Components;
using ParkIstra.Models.Main;

namespace ParkIstra.AppBlazor.Client.Pages.Member.Login;
public partial class RegisterPage
{
    //#region Properties

    //[AllowNull]
    //private ParkIstra.Models.Main.Register RegisterModel { get; set; } = new();
    //private string? ConfirmPassword { get; set; }
    private BlazorProblemDetails? BlazorProblemDetails { get; set; }
    //private bool isSuccess { get; set; }
    //private bool IsFizickoLice { get; set; }

    //#endregion

    private Register RegisterModel = new Register();

    private async Task HandleValidSubmit()
    {
        var response = await MainApiService.RegisterAsync(RegisterModel.Email, RegisterModel.Password, (int)RegisterModel.UserType);
        BlazorProblemDetails = response.BlazorProblemDetails;
        if (BlazorProblemDetails != null || response.Single.Status == false)
        {
            if (response.Single.Message.Equals("Invalid model state"))
            {
                await ErrorHandler.HandleRequestError("Već postoji korisnik.");
            }
            return;
        }
        await ErrorHandler.HandleEmailSuccess("Uspješno je kreiran nalog. Potvrdite mail kako bi mogli da se ulogujete.");
        this.StateHasChanged();
        // Process the registration data
        // Example: await Http.PostAsJsonAsync("api/register", registerModel);
    }

    protected override async Task OnInitializedAsync()
    {
    }

    //private async Task Submit()
    //{
    //    var response = await MainApiService.RegisterAsync(RegisterModel);
    //    BlazorProblemDetails = response.BlazorProblemDetails;
    //    if (BlazorProblemDetails != null || response.Single.Status == false)
    //    {
    //        if (response.Single.Message.Equals("Invalid model state"))
    //        {
    //            await ErrorHandler.HandleRequestError("Već postoji korisnik.");
    //        }
    //        return;
    //    }
    //    await ErrorHandler.HandleEmailSuccess("Uspješno je kreiran nalog. Potvrdite mail kako bi mogli da se ulogujete.");
    //    //isSuccess = true;
    //    this.StateHasChanged();
    //}
    private void GoBack()
    {
        NavigationManager.NavigateTo("/");
    }

    [Inject, AllowNull]
    private IMainApiService MainApiService { get; set; }
    [Inject, AllowNull]
    private ErrorHandler ErrorHandler { get; set; }
}