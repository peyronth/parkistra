namespace ParkIstra.AppServices.MainApi;

public partial class MainApiService
{

    public async Task<Response<Response>> RegisterAsync(string Email, string Password, int UserType) =>
        await MainApiBroker.RegisterAsync($"Authentication/register?email={Email}&Password={Password}&UserType={UserType}");
    public async Task<Response<Response>> LoginAsync(string Email, string Password) =>
        await MainApiBroker.LoginAsync($"Authentication/login?Email={Email}&Password={Password}");
    public async Task<Response<Response>> SendResetPwdLink(string email) =>
        await MainApiBroker.SendResetPwdLink($"Authentication/resetPassword?email={email}");
    public async Task<Response<Response>> ConfirmPwdLink(string email, string token, string newPassword) =>
        await MainApiBroker.ConfirmPwdLink($"Authentication/confirmresetpassword?email={email}&token={token}&newPassword={newPassword}");
    public async Task<Response<Response>> ConfirmEmail(string token, string email) =>
        await MainApiBroker.ConfirmEmail($"Authentication/ConfirmEmailLink?token={token}&email={email}");
    public async Task<Response<ApplicationUser>> GetUserByEmail(string email) =>
        await MainApiBroker.GetUserByEmail($"Authentication/getuser?email={email}");


    private static Register GetPreparedRegsiter(Register user)
    {
        var preparedRegister = JsonSerializer.Deserialize<Register>(
            JsonSerializer.Serialize(user))!;

        return preparedRegister;
    }
    private static Login GetPreparedLogin(Login model)
    {
        var preparedLogin = JsonSerializer.Deserialize<Login>(
            JsonSerializer.Serialize(model))!;

        return preparedLogin;
    }
}