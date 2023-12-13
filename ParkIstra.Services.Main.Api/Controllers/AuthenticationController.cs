using System;
using System.Security.Policy;

namespace ParkIstra.Services.MainApi.Controllers;

[Route("[controller]")]
// [ApiController]
public class AuthenticationController : ControllerBase, IDisposable
{
    public AuthenticationController(
        IDbContextFactory<MainDbContext> mainDbContextFactory,
        ASPProblemDetailsFactory aspProblemDetailsFactory,
        UserManager<ApplicationUser> userMgr,
        SignInManager<ApplicationUser> signinMgr,
        JwtConfiguration jwtConfiguration,
        IEmailSender emailSender
    )
    {
        MainDbContext = mainDbContextFactory.CreateDbContext();
        MainDbContextFactory = mainDbContextFactory;
        ProblemFactory = aspProblemDetailsFactory;
        userManager = userMgr;
        signInManager = signinMgr;
        _jwtConfiguration = jwtConfiguration;
        _emailSender = emailSender;
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register(Register user)
    {
        if (ModelState.IsValid)
        {
            ApplicationUser appUser = new ApplicationUser
            {
                UserName = user.Email,
                Email = user.Email
            };

            IdentityResult result = await userManager.CreateAsync(appUser, user.password);

            if (result.Succeeded)
            {
                var token = HttpUtility.UrlEncode(await userManager.GenerateEmailConfirmationTokenAsync(appUser));
                var confirmationlink = "https://localhost:7158/Authentication/ConfirmEmailLink?token=" + token + "&email=" + user.Email;

                var message = new Messages(new string[] { appUser.Email },
                    "Potvrda email adrese",
                    "Potvrdite ovaj email klikom ispod kako biste aktivirali nalog.",
                    "Poštovani,",
                    $"{confirmationlink}",
                    "Ne brinite ukoliko niste zahtijevali potvrdu emaila. I dalje je zaštićen i niko nema pristup." +
                    "Najvjerovatnije - neko je greškom ukucao ovaj email prilikom kreiranja naloga.");
                _emailSender.SendEmailAsync(message);
            }
            var err = "";
            if (result.Succeeded)
                return Ok(new Response
                {
                    Status = true,
                    Message = "Account created successfuly.",
                    StatusCode = System.Net.HttpStatusCode.OK.ToString(),
                    Data = "Confirm your email to continue."
                });
            else
            {
                foreach (IdentityError error in result.Errors)
                    err = err + "," + error.Description;

                return Ok(new Response
                {
                    Status = false,
                    Message = "Error.",
                    StatusCode = System.Net.HttpStatusCode.BadRequest.ToString(),
                    Data = err
                });
            }
        }
        return Ok(new Response
        {
            Status = false,
            Message = "Invalid model state",
            StatusCode = System.Net.HttpStatusCode.BadRequest.ToString()
        });
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(Login model)
    {
        var user = userManager.Users.Include(x => x.UserInfo).Where(x => x.Email == model.Email).FirstOrDefault();
        var canSignIn = await signInManager.CanSignInAsync(user);
        if (!canSignIn)
        {
            return BadRequest(new Response
            {
                Status = false,
                Message = "Confirm email",
                StatusCode = System.Net.HttpStatusCode.UnavailableForLegalReasons.ToString(),
                Data = ""
            });
        }
        if (user != null && await userManager.CheckPasswordAsync(user, model.password))
        {
            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

            var token = GetToken(authClaims);
            return Ok(new Response()
            {
                Status = true,
                token = new JwtSecurityTokenHandler().WriteToken(token),
                id = user.Id.ToString(),
                FullName = "Andrija and Thomas",
            });
        }
        return Unauthorized();
    }

    [HttpGet]
    [Route("resetPassword")]
    public async Task<Response> SendResetPwdLink(string email)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user != null)
        {
            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            var code = HttpUtility.UrlEncode(token);
            var link = "https://localhost:7158/change-password?";
            var buillink = link + "email=" + email + "&token=" + code;

            var message = new Messages(new string[] { email },
                "Change of password",
                "In order to change password, click the link bellow and enter the new password.",
                "To whom it may concern,",
                $"{buillink}",
                "Do not worry if you haven't asked for password change. Someone must have accidentaly entered your email, but if he" +
                " does not have access to your email, there is nothing to worry about.");
            _emailSender.SendEmailAsync(message);

            return new Response
            {
                Status = true,
                Message = "Link Sent Succesfully",
                StatusCode = System.Net.HttpStatusCode.OK.ToString(),
                Data = buillink
            };
        }
        else
        {
            return new Response
            {
                Status = false,
                Message = "Invalid email",
                StatusCode = System.Net.HttpStatusCode.BadRequest.ToString(),
                Data = null
            };
        }

    }

    [HttpGet]
    [Route("confirmresetpassword")]
    public async Task<Response> ConfirmPwdLink(string email, string token, string newPassword)
    {
        var user = await userManager.FindByEmailAsync(email);
        var result = await userManager.ResetPasswordAsync(user, token, newPassword);
        if (!result.Succeeded)
        {
            return new Response
            {
                Status = false,
                Message = "Invalid Request",
                StatusCode = System.Net.HttpStatusCode.UnprocessableEntity.ToString(),
            };
        }
        else
        {
            return new Response
            {
                Status = true,
                Message = "Your Password has been succesfully updated",
                StatusCode = System.Net.HttpStatusCode.OK.ToString()
            };
        }
    }
    [HttpGet]
    [Route("ConfirmEmailLink")]
    public async Task<Response> ConfirmEmail(string token, string email)
    {
        var user = await userManager.FindByEmailAsync(email);
        var result = await userManager.ConfirmEmailAsync(user, token);
        if (!result.Succeeded)
        {
            return new Response
            {
                Status = false,
                Message = "Invalid Token",
                StatusCode = System.Net.HttpStatusCode.OK.ToString(),
            };
        }
        else
        {
            return new Response
            {
                Status = true,
                Message = "Your Email Confirmed Succesfully",
                StatusCode = System.Net.HttpStatusCode.OK.ToString(),
            };
        }

    }
    private JwtSecurityToken GetToken(List<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration.Secret));
        var token = new JwtSecurityToken(
        issuer: _jwtConfiguration.ValidIssuer,
            audience: _jwtConfiguration.ValidAudience,
            expires: DateTime.UtcNow.AddDays(90),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );
        return token;
    }

    [HttpGet]
    [Route("getuser")]
    public async Task<ApplicationUser> GetUserByEmail(string email)
    {
        var user = await userManager.FindByEmailAsync(email);
        return user;
    }



    public void Dispose()
    {
        MainDbContext.Dispose();
    }
    private MainDbContext MainDbContext;
    private UserManager<ApplicationUser> userManager;
    private SignInManager<ApplicationUser> signInManager;
    private readonly IEmailSender _emailSender;
    private readonly JwtConfiguration _jwtConfiguration;
    private int count;
    private IDbContextFactory<MainDbContext> MainDbContextFactory { get; init; }

    private ASPProblemDetailsFactory ProblemFactory { get; init; }
}
