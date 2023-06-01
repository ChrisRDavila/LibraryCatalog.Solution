using Microsoft.AspNetCore.Mvc;
using Library.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Library.Controllers
{
  [Authorize(Roles = "Admin")]
  public class PatronsController : Controller
  {
    public readonly LibraryContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public PatronsController(UserManager<ApplicationUser> userManager, LibraryContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public async Task<ActionResult> Index()
    {
      string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
      List<Patron> model = _db.Patrons.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Patron patron)
    {
      if (!ModelState.IsValid)
      {
        return View(patron);
      }
      else
      {
        string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
        patron.User = currentUser;
        _db.Patrons.Add(patron);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }
    }

    public ActionResult Details(int id)
    {
      Patron thisPatron = _db.Patrons
                            .Include(patron => patron.JoinBookPatron)
                            .ThenInclude(join => join.Book)
                            .Include(patron => patron.JoinPatronCheckout)
                            .ThenInclude(join => join.Checkout)
                            .FirstOrDefault(patron => patron.PatronId == id);
      if (thisPatron == null)
      {
        return RedirectToAction("Index");
      }
      return View(thisPatron);
    }

    public ActionResult Edit(int id)
    {
      Patron thisPatron = _db.Patrons.FirstOrDefault(patron => patron.PatronId == id);
      return View(thisPatron);
    }

    [HttpPost]
    public ActionResult Edit(Patron patron)
    {
      _db.Entry(patron).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddBook(int id)
    {
      Patron thisPatron = _db.Patrons.FirstOrDefault(patron => patron.PatronId == id);
      ViewBag.BookId = new SelectList(_db.Books, "BookId", "Title");
      return View(thisPatron);
    }

    [HttpPost]
    public ActionResult AddBook(Patron patron, int bookId)
    {
      #nullable enable
      BookPatron? joinEntity = _db.BookPatrons.FirstOrDefault(join => (join.BookId == bookId && join.PatronId == patron.PatronId));
      #nullable disable
      if (joinEntity == null && bookId != 0)
      {
        _db.BookPatrons.Add(new BookPatron() { BookId = bookId, PatronId = patron.PatronId });
        _db.SaveChanges();
      }
      
      return RedirectToAction("Details", new { id = patron.PatronId });
    }

    public ActionResult AddCheckout(int id)
    {
      Patron thisPatron = _db.Patrons.FirstOrDefault(patron => patron.PatronId == id);
      ViewBag.CheckoutId = new SelectList(_db.Checkouts, "CheckoutId", "CheckoutDate");
      return View(thisPatron);
    }

    [HttpPost]
    public ActionResult AddCheckout(Patron patron, int checkoutId)
    {
      #nullable enable
      
      PatronCheckout? joinEntity = _db.PatronCheckouts.FirstOrDefault(join => (join.CheckoutId == checkoutId && join.PatronId == patron.PatronId));
      #nullable disable
      if (joinEntity == null && checkoutId != 0)
      {
        _db.PatronCheckouts.Add(new PatronCheckout() { CheckoutId = checkoutId, PatronId = patron.PatronId });
        _db.SaveChanges();
      }
      
      return RedirectToAction("Details", new { id = patron.PatronId });
    }

    public ActionResult Delete(int id)
    {
      Patron thisPatron = _db.Patrons.FirstOrDefault(patron => patron.PatronId == id);
      return View(thisPatron);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Patron thisPatron = _db.Patrons.FirstOrDefault(patron => patron.PatronId == id);
      _db.Patrons.Remove(thisPatron);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}