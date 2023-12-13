namespace ParkIstra.AppServices.MainApi;

public partial class MainApiService
{

    public async Task<Response<Response>> RegisterAsync(ParkIstra.Models.Main.Register user) =>
        await MainApiBroker.RegisterAsync($"Authentication/register", GetPreparedRegsiter(user));
    public async Task<Response<Response>> LoginAsync(ParkIstra.Models.Main.Login model) =>
        await MainApiBroker.LoginAsync($"Authentication/login", GetPreparedLogin(model));
    public async Task<Response<Response>> SendResetPwdLink(string email) =>
        await MainApiBroker.SendResetPwdLink($"Authentication/resetPassword?email={email}");
    public async Task<Response<Response>> ConfirmPwdLink(string email, string token, string newPassword) =>
        await MainApiBroker.ConfirmPwdLink($"Authentication/confirmresetpassword?email={email}&token={token}&newPassword={newPassword}");
    public async Task<Response<Response>> ConfirmEmail(string token, string email) =>
        await MainApiBroker.ConfirmEmail($"Authentication/ConfirmEmailLink?token={token}&email={email}");
    public async Task<Response<ApplicationUser>> GetUserByEmail(string email) =>
        await MainApiBroker.GetUserByEmail($"Authentication/getuser?email={email}");


    private static ParkIstra.Models.Main.Register GetPreparedRegsiter(ParkIstra.Models.Main.Register user)
    {
        var preparedRegister = JsonSerializer.Deserialize<ParkIstra.Models.Main.Register>(
            JsonSerializer.Serialize(user))!;

        return preparedRegister;
    }
    private static ParkIstra.Models.Main.Login GetPreparedLogin(ParkIstra.Models.Main.Login model)
    {
        var preparedLogin = JsonSerializer.Deserialize<ParkIstra.Models.Main.Login>(
            JsonSerializer.Serialize(model))!;

        return preparedLogin;
    }
}