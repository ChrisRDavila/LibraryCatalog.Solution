using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Library.Models;
using System.Threading.Tasks;
using Library.ViewModels;

namespace Library.Controllers
{
  public class AccountsController : Controller
  {
    private readonly LibraryContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AccountsController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, LibraryContext db)
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _db = db;
    }

    public ActionResult Index()
    {
      return View();
    }

    public ActionResult Register()
    {
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
      if (!ModelState.IsValid)
      {
          return View(model);
      }

      var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
      var result = await _userManager.CreateAsync(user, model.Password);

      if (result.Succeeded)
      {
          if (model.IsAdmin)
          {
              if (model.AdminPassword != "admin")
              {
                  ModelState.AddModelError(nameof(model.AdminPassword), "The admin password is incorrect.");
                  return View(model);
              }
              
              await _userManager.AddToRoleAsync(user, "Admin");
              return RedirectToAction("Index", "Accounts");
          }
          else
          {
              await _userManager.AddToRoleAsync(user, "User");
              return RedirectToAction("Index", "Accounts");
          }
      }

      foreach (var error in result.Errors)
      {
          ModelState.AddModelError("", error.Description);
      }

      return View(model);
    }


    public ActionResult Login()
    {
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Login(LoginViewModel model)
    {
      if (!ModelState.IsValid)
      {
        return View(model);
      }
      else
      {
        Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: true, lockoutOnFailure: false);
        if (result.Succeeded)
        {
          return RedirectToAction("Index");
        }
        else
        {
          ModelState.AddModelError(string.Empty, "Invalid login attempt.");
          return View(model);
        }
      }
    }

    [HttpPost]
    public async Task<ActionResult> LogOff()
    {
      await _signInManager.SignOutAsync();
      return RedirectToAction("Index");
    }
  }
}