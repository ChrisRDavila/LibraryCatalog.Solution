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
  [Authorize]
  public class AuthorsController : Controller
  {
    private readonly LibraryContext _db;

    private readonly UserManager<ApplicationUser> _userManager;

    
    public AuthorsController(UserManager<ApplicationUser> userManager, LibraryContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
      string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
      List<Author> model = _db.Authors.ToList();
      return View(model);
    }

    [Authorize(Roles = "Admin")]
    public ActionResult Create()
    {
      return View();
    }
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult> Create(Author author, int BookId)
    {
      if (!ModelState.IsValid)
      {
        ViewBag.BookId = new SelectList(_db.Books, "BookId", "Title");
        return View(author);
      }
      else
      {
        string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
        author.User = currentUser;
        _db.Authors.Add(author);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }
    }
    [Authorize(Roles = "Admin, User")]
    public ActionResult Details(int id)
    {
      Author thisAuthor = _db.Authors
                              .Include(author => author.JoinEntities)
                              .ThenInclude(join => join.Book)
                              .FirstOrDefault(author => author.AuthorId == id);
      return View(thisAuthor);
    }
    [Authorize(Roles = "Admin")]
    public ActionResult Edit(int id)
    {
      Author thisAuthor = _db.Authors.FirstOrDefault(author => author.AuthorId == id);
      ViewBag.BookId = new SelectList(_db.Books, "BookId", "Title");
      return View(thisAuthor);
    }
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public ActionResult Edit(Author author)
    {
      _db.Authors.Update(author);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    [Authorize(Roles = "Admin")]
    public ActionResult Delete(int id)
    {
      Author thisAuthor = _db.Authors.FirstOrDefault(author => author.AuthorId == id);
      return View(thisAuthor);
    }
    [Authorize(Roles = "Admin")]
    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Author thisAuthor = _db.Authors.FirstOrDefault(author => author.AuthorId == id);
      _db.Authors.Remove(thisAuthor);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    [Authorize(Roles = "Admin")]
    public ActionResult AddBook(int id)
    {
      Author thisAuthor = _db.Authors.FirstOrDefault(author => author.AuthorId == id);
      ViewBag.BookId = new SelectList(_db.Books, "BookId", "Title");
      return View(thisAuthor);
    }
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public ActionResult AddBook(Author author, int BookId)
    {
      #nullable enable
      AuthorBook? joinEntity = _db.AuthorBooks.FirstOrDefault(join => join.AuthorId == author.AuthorId && join.BookId == BookId);
      #nullable disable
      if (joinEntity == null && BookId != 0)
      {
        _db.AuthorBooks.Add(new AuthorBook() { AuthorId = author.AuthorId, BookId = BookId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = author.AuthorId });
    }
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public ActionResult DeleteJoin(int joinId)
    {
      AuthorBook joinEntry = _db.AuthorBooks.FirstOrDefault(entry => entry.AuthorBookId == joinId);
      _db.AuthorBooks.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    [Authorize(Roles = "Admin, User")]
    [HttpPost, ActionName("Search")]
    public ActionResult Search(string search)
    {
      List<Author> model = _db.Authors.Where(author => author.AFirstName.ToLower().Contains(search.ToLower())).ToList();
      return View(model);
    }
  }
}