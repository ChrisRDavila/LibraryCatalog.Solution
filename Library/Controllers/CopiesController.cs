using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Library.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;
using System;

namespace Library.Controllers
{
  [Authorize]
  public class CopiesController : Controller
  {
    private readonly LibraryContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public CopiesController(UserManager<ApplicationUser> userManager, LibraryContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public async Task<ActionResult> Index()
    {
      string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
      List<Copy> userCopies = _db.Copies
                          .Where(entry => entry.User.Id == currentUser.Id)
                          .ToList();
      return View(userCopies);
    }

    public ActionResult Create()
    {
      return View();
    }
    
    [HttpPost]
    public async Task<ActionResult> Create(Copy copy)
    {
      string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
      copy.User = currentUser;
      _db.Copies.Add(copy);
      _db.SaveChanges();
      return RedirectToAction("Index");
      
    }

    public ActionResult Details(int id)
    {
      Copy thisCopy = _db.Copies
          .Include(copy => copy.JoinEntities)
          .ThenInclude(join => join.Patron)
          .FirstOrDefault(copy => copy.CopyId == id);
      return View(thisCopy);
    }

    public ActionResult Edit(int id)
    {
      Copy thisCopy = _db.Copies.FirstOrDefault(copies => copies.CopyId == id);
      return View(thisCopy);
    }

    [HttpPost]
    public ActionResult Edit(Copy copy)
    {
      _db.Copies.Update(copy);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddPatron(int id)
    {
      Copy thisCopy = _db.Copies.FirstOrDefault(copies => copies.CopyId == id);
      ViewBag.PatronId = new SelectList(_db.Patrons, "PatronId", "PName");
      return View(thisCopy);
    }

    [HttpPost]
    public ActionResult CheckoutBook(int copyId, int patronId)
    {
      CopyPatron copyPatron = new CopyPatron
      {
        CopyId = copyId,
        PatronId = patronId,
        Status = true,
        CheckoutDate = DateTime.Now,
        DueDate = DateTime.Now.AddDays(14)
      };
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Copy thisCopy = _db.Copies.FirstOrDefault(copies => copies.CopyId == id);
      return View(thisCopy);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Copy thisCopy = _db.Copies.FirstOrDefault(copies => copies.CopyId == id);
      _db.Copies.Remove(thisCopy);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}
