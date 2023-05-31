using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Library.Models;
using System.Threading.Tasks;
using Library.ViewModels;

namespace Library.Controllers
{
  public class AccountController : Controller
  {
    private readonly LibraryContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, LibraryContext db)
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
    public async Task<ActionResult> Register(AssignRoleViewModel model)
    {
      if (!ModelState.IsValid)
      {
          return View(model);
      }

      if (model.AdminPassword != "adminpassword")
      {
          ModelState.AddModelError("", "Invalid admin password.");
          return View(model);
      }

      ApplicationUser user = new ApplicationUser { UserName = model.UserName };
      
      IdentityResult result = await _userManager.CreateAsync(user, model.AdminPassword);
      if (result.Succeeded)
      {
          var assignRoleResult = await _userManager.AddToRoleAsync(user, "Admin");
          if (assignRoleResult.Succeeded)
          {
              return RedirectToAction("AssignRole", "Role", new { id = user.Id });
          }
          else
          {
              foreach (var error in assignRoleResult.Errors)
              {
                  ModelState.AddModelError("", error.Description);
              }
              return View(model);
          }
      }
      else
      {
          foreach (IdentityError error in result.Errors)
          {
              ModelState.AddModelError("", error.Description);
          }
          return View(model);
      }
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