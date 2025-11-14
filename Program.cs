using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// alternatively: builder.UseStartup<Startup>();
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.Unspecified;
    // Handling SameSite cookie according to https://learn.microsoft.com/aspnet/core/security/samesite?view=aspnetcore-3.1
    options.HandleSameSiteCookieCompatibility();
});


// Configuration to sign-in users with Azure AD B2C
builder.Services.AddMicrosoftIdentityWebAppAuthentication(builder.Configuration, "AzureAdB2C");
Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;
Microsoft.IdentityModel.Logging.IdentityModelEventSource.LogCompleteSecurityArtifact = true;

builder.Services.AddControllersWithViews()
        .AddMicrosoftIdentityUI();

builder.Services.AddRazorPages();

//Configuring appsettings section AzureAdB2C, into IOptions
builder.Services.AddOptions();
builder.Services.Configure<OpenIdConnectOptions>(builder.Configuration.GetSection("AzureAdB2C"));

// Customize OpenIdConnectOptions
builder.Services.Configure<OpenIdConnectOptions>(OpenIdConnectDefaults.AuthenticationScheme, options =>
{    
    options.Scope.Clear();
    options.Scope.Add("openid");
    options.Scope.Add("profile");
    options.Scope.Add("offline_access");

    // options.Scope.Add("f8f43193-dc83-49f2-a16a-27137bdd9767");
    // causes error: AADB2C90146: The scope provided in request specifies more than one resource for an access token, which is not supported.
    
    options.Scope.Add("https://samplemwb2ctenant.onmicrosoft.com/753fd60d-bf11-48d8-9ef4-31ab1e3aac21/access"); // WebAPI app
    options.Events.OnTokenValidated = async context =>
    {
        // pobranie ID token
        var idToken = context.SecurityToken as System.IdentityModel.Tokens.Jwt.JwtSecurityToken;
        var rawIdToken = idToken.RawData;
        

        Console.WriteLine("ID Token:");
        Console.WriteLine(rawIdToken);

        var tokenResponse = context.TokenEndpointResponse;
        var accessToken = tokenResponse.AccessToken;

        Console.WriteLine("Access Token:");
        Console.WriteLine(accessToken);
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Add the Microsoft Identity Web cookie policy
app.UseCookiePolicy();
app.UseRouting();

// Add the ASP.NET Core authentication service
app.UseAuthentication();
app.UseAuthorization();

// alernative to this code is on https://learn.microsoft.com/en-us/azure/active-directory-b2c/enable-authentication-web-application?tabs=visual-studio
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
