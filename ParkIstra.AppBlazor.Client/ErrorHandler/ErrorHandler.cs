using ParkIstra.Models.Util;
using System.Diagnostics.CodeAnalysis;
using ParkIstra.Libraries.Blazor.Components;
using ParkIstra.AppBlazor.Client.Components;
using ParkIstra.AppInterfaces;
using ParkIstra.Libraries.Blazor;

namespace ParkIstra.AppBlazor.Client;

public partial class ErrorHandler
{

    [AllowNull]
    public static Dialog ErrorDialog { get; set; } = new();

    [AllowNull]
    public static SuccessDialog successDialog { get; set; } = new();

    public ErrorHandler(IUtilApiService utilApiService)
    {
        UtilApiService = utilApiService;
    }

    public async Task HandleRequestError(string msg)
    {

        await ErrorDialogAsync("Greska", msg);

    }
    public async Task HandleEmailSuccess(string msg)
    {

        await ConfirmDialogAsync("Uspješno", msg);

    }

    public async Task HandleError(BlazorProblemDetails pd, string msg, bool IsDevelopment)
    {

        // Ukoliko je development mode, prikazi detaljnu poruku.
        if (IsDevelopment)
        {
            await ErrorDialogAsync(pd.Instance ?? "Greška", pd.Title ?? "Desila se greška.");
        }
        // Ukoliko je produkcija, prikazi jednostavnu poruku
        else
        {
            string errorType = pd.Title;
            errorType = errorType switch
            {
                string a when a.ToLower().Contains("reference") => " jer se odabrani podatak koristi u drugim tabelama.",
                string a when a.ToLower().Contains("unique") => ", vrijednost mora biti jedinstvena.",
                string a when a.ToLower().Contains("unexpected") => ", neocekivan broj redova izmijenjen",
                _ => ""
            };

            if (pd.Title.ToLower().Contains("number (0) of rows."))
            {

            }
            else if (!string.IsNullOrEmpty(errorType))
            {
                msg += errorType;
                await ErrorDialogAsync("Obavještenje ", msg);

            }


        }
        var response = await UtilApiService.AddSYS_ExceptionAsync(
                    new SYS_Exception(pd.Instance, pd.Title)
                    );

    }

    public async Task ShowSuccessMessage(int IdNav, string BrPredmeta)
    {
        await SuccessDialogAsync(IdNav, BrPredmeta);
    }

    public static async Task ErrorDialogAsync(string title, string msg)
    {

        var options = new DialogOptions
        {
            DialogTitle = title,
            DialogMessage = msg,
            OkButtonText = "U redu",
            DialogType = DialogType.Dangerous,

        };
        await ErrorDialog.ShowAsync(options);


    }

    public static async Task ConfirmDialogAsync(string title, string msg)
    {

        var options = new DialogOptions
        {
            DialogTitle = title,
            DialogMessage = msg,
            OkButtonText = "U redu",
            DialogType = DialogType.Informational,

        };
        await ErrorDialog.ShowAsync(options);


    }

    public static async Task SuccessDialogAsync(int NavID, string BrPredmeta)
    {

        var options = new SuccessDialogOptions
        {
            DialogTitle = "Obavjestenje",
            DialogMessage = $"Uspjesno je dupliran predmet - {BrPredmeta}",
            OkButtonText = "Otvori predmet",
            DialogType = SucessDialogType.Normal,
            NavigationId = NavID,

        };
        await successDialog.ShowAsync(options);

    }
    private IUtilApiService UtilApiService { get; set; }
}