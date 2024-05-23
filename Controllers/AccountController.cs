using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyApp.Namespace;

namespace project;

public class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;

    public AccountController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public IActionResult Login()
    {
        return View(new LoginModel());
    }


    [HttpPost]
    public async Task<IActionResult> LoginPost(LoginModel model)
    {
        if (!ModelState.IsValid)
        {
            return View("Login", model);
        }

        var user = await _userManager.FindByEmailAsync(model.Input.Email);

        if (user == null)
        {
            ModelState.AddModelError("Input.Email", "Invalid login attempt.");
            return View("Login", model);
        }

        var result = PasswordHasher.VerifyPassword(model.Input.Password, user.PasswordHash ?? "");

        if (!result)
        {
            ModelState.AddModelError("Input.Password", "Invalid login attempt.");
            return View("Login", model);
        }

        await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Id)
        }, IdentityConstants.ApplicationScheme)));

        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
        return RedirectToAction("Login", "Account", new { area = "", returnUrl = "", });
    }
}
