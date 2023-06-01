using Microsoft.AspNetCore.Mvc;
using Library.Models;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;


namespace Library.Controllers
{
  [Authorize(Roles = "Admin, User")]
  public class CheckoutController : Controller
  {
    private readonly LibraryContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public CheckoutController(UserManager<ApplicationUser> userManager, LibraryContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public async Task<ActionResult> Index()
    {
      string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
      List<Checkout> checkouts = _db.Checkouts.ToList();
      return View(checkouts);
    }

    public ActionResult Create(int copyId)
    {
      Copy copy = _db.Copies.FirstOrDefault(c => c.CopyId == copyId);
      if (copy == null)
      {
        return NotFound();
      }

      Checkout checkout = new Checkout
      {
        CopyId = copy.CopyId,
        CheckoutDate = DateTime.Now,
        DueDate = DateTime.Now.AddDays(14),
        Status = true
      };

      return View(checkout);
    }

    [HttpPost]
    public async Task<ActionResult> Create(Checkout checkout)
    {
      if (!ModelState.IsValid)
      {
        return View(checkout);
      }
      string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
      checkout.User = currentUser;
      _db.Checkouts.Add(checkout);
      _db.SaveChanges();

      return RedirectToAction("Index");
    }

    public ActionResult Return(int id)
    {
      Checkout checkout = _db.Checkouts.FirstOrDefault(c => c.CheckoutId == id);
      if (checkout == null)
      {
        return NotFound();
      }

      return View(checkout);
    }

    [HttpPost]
    public ActionResult Return(int id, DateTime returnDate)
    {
      Checkout checkout = _db.Checkouts.FirstOrDefault(c => c.CheckoutId == id);
      if (checkout == null)
      {
        return NotFound();
      }

      if (returnDate > checkout.DueDate)
      {
        checkout.IsOverdue = true;
      }

      checkout.Status = false;
      _db.SaveChanges();

      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Checkout checkout = _db.Checkouts.FirstOrDefault(c => c.CheckoutId == id);
      if (checkout == null)
      {
        return NotFound();
      }

      return View(checkout);
    }
  }
}