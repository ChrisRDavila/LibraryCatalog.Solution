using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Library.Models;
using Library.ViewModels;
using System.Collections.Generic;

namespace Library.Controllers
{
  public class RoleController : Controller
  {
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public RoleController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
    {
      _roleManager = roleManager;
      _userManager = userManager;
    }

    public IActionResult Index()
    {
      var roles = _roleManager.Roles;
      return View(roles);
    }

    public IActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(IdentityRole role)
    {
      if (ModelState.IsValid)
      {
        var result = await _roleManager.CreateAsync(role);
        if (result.Succeeded)
        {
          return RedirectToAction("Index");
        }
        else
        {
          foreach (var error in result.Errors)
          {
            ModelState.AddModelError("", error.Description);
          }
        }
      }
      return View(role);
    }

    public async Task<IActionResult> Edit(string id)
    {
      var role = await _roleManager.FindByIdAsync(id);
      if (role == null)
      {
        return NotFound();
      }
      return View(role);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(IdentityRole role)
    {
      if (ModelState.IsValid)
      {
        var result = await _roleManager.UpdateAsync(role);
        if (result.Succeeded)
        {
          return RedirectToAction("Index");
        }
        else
        {
          foreach (var error in result.Errors)
          {
            ModelState.AddModelError("", error.Description);
          }
        }
      }
      return View(role);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(string id)
    {
      var role = await _roleManager.FindByIdAsync(id);
      if (role != null)
      {
        var result = await _roleManager.DeleteAsync(role);
        if (result.Succeeded)
        {
          return RedirectToAction("Index");
        }
      }
      return NotFound();
    }

    public async Task<IActionResult> AssignRole(string id)
    {
      var user = await _userManager.FindByIdAsync(id);
      if (user == null)
      {
        return NotFound();
      }

      var roles = _roleManager.Roles;
      var userRoles = await _userManager.GetRolesAsync(user);

      var model = new AssignRoleViewModel
      {
        UserId = user.Id,
        UserName = user.UserName,
        UserRoles = userRoles,
        Roles = roles
      };
      return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> AssignRole(AssignRoleViewModel model)
    {
      var user = await _userManager.FindByIdAsync(model.UserId);
      if (user == null)
      {
        return NotFound();
      }

      var currentRoles = await _userManager.GetRolesAsync(user);
      var result = await _userManager.RemoveFromRolesAsync(user, currentRoles);

      if (result.Succeeded)
      {
        result = await _userManager.AddToRolesAsync(user, model.UserRoles);
        if (result.Succeeded)
        {
          return RedirectToAction("Index");
        }
      }

      ModelState.AddModelError("", "Failed to assign roles");

      model.UserRoles = currentRoles;
      model.Roles = _roleManager.Roles;

      return View(model);
    }
  }
}