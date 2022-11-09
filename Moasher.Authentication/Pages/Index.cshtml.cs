using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moasher.Authentication.Core.IdentityServer;

namespace Moasher.Authentication.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public IActionResult OnGet()
    {
        var defaultClientUrl = Config.Clients.First().AllowedCorsOrigins.First();
        return Redirect(defaultClientUrl);
    }
}