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

    public IActionResult Index()
    {
        IConfidentialClientApplication msalClient = ConfidentialClientApplicationBuilder.Create(_oidcOptions.Value.ClientId)
                    .WithClientSecret(_oidcOptions.Value.ClientSecret)
                    .WithAuthority(new Uri(_oidcOptions.Value.Authority))
                    .Build();

        msalClient.AddInMemoryTokenCache();
        msalClient.AcquireTokenForClient(new string[] { "https://graph.microsoft.com/.default" }).WithExtraQueryParameters("OnBehalfOf=user456").ExecuteAsync();

        //AuthenticationResult msalAuthenticationResult = await msalClient.AcquireTokenForClient(new string[] { "https://graph.microsoft.com/.default" }).ExecuteAsync();

        return View();
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
