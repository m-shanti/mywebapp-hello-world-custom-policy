using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mywebapp.Models;
using Microsoft.Identity.Client;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;

namespace mywebapp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IOptions<OpenIdConnectOptions> _oidcOptions;

    public HomeController(ILogger<HomeController> logger, IOptions<OpenIdConnectOptions> oidcOptions)
    {
        _logger = logger;
        _oidcOptions = oidcOptions;
    }

    public async Task<IActionResult> Index()
    {
        string token = await GetClientCredentialsTokenAsync();
        return Content(token);
    }

    private void GetTokenWithStandardAd()
    {
        IConfidentialClientApplication msalClient = ConfidentialClientApplicationBuilder.Create(_oidcOptions.Value.ClientId)
            .WithClientSecret(_oidcOptions.Value.ClientSecret)
            .WithAuthority(_oidcOptions.Value.Authority)
            .Build();

        msalClient.AddInMemoryTokenCache();
        AuthenticationResult authResult = msalClient.AcquireTokenForClient(new string[] { "https://samplemwb2ctenant.onmicrosoft.com/753fd60d-bf11-48d8-9ef4-31ab1e3aac21/.default" })
            .WithExtraQueryParameters("OnBehalfOf=user456")
            .ExecuteAsync().Result;

        Console.WriteLine("Access token OBO");
        Console.WriteLine(authResult.AccessToken);
    }

    private async Task<string> GetClientCredentialsTokenAsync()
    {
        //var tokenEndpoint =           $"{_oidcOptions.Instance}/{_oidcOptions.Domain}/{_oidcOptions.SignUpSignInPolicyId}/oauth2/v2.0/token";
        var tokenEndpoint = "https://samplemwb2ctenant.b2clogin.com/samplemwb2ctenant.onmicrosoft.com/B2C_1A_ClientCredentialsCustomPolicyPlusCustomParam_H/oauth2/v2.0/token";

        using var client = new HttpClient();

        var parameters = new Dictionary<string, string>
        {
            { "client_id", _oidcOptions.Value.ClientId },
            { "client_secret", _oidcOptions.Value.ClientSecret },
            { "grant_type", "client_credentials" },
            { "scope", "https://samplemwb2ctenant.onmicrosoft.com/753fd60d-bf11-48d8-9ef4-31ab1e3aac21/.default" },
            { "OnBehalfOf", "user456"}
        };

        var response = await client.PostAsync(tokenEndpoint, new FormUrlEncodedContent(parameters));

        //response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        return json;
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [Authorize]
    public IActionResult Claims()
    {
        return View();
    }
}
