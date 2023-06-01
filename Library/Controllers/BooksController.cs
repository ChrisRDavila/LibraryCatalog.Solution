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
  public class BooksController : Controller
  {
    private readonly LibraryContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public BooksController(UserManager<ApplicationUser> userManager, LibraryContext db)
    {
      _userManager = userManager;
      _db = db;
    }
    [AllowAnonymous]
    public async Task<IActionResult> Index(string searchString)
    {
      
      ApplicationUser currentUser = await _userManager.GetUserAsync(User);

      List<Book> userBooks;

      if (User.IsInRole("Admin"))
      {
        userBooks = _db.Books
          .Include(book => book.JoinAuthorBook)
          .Include(book => book.JoinBookCopy)
          .ToList();
      }
      else
      {
      userBooks = _db.BookPatrons
          .Where(bp => bp.PatronId.ToString() == currentUser.Id)
          .Select(bp => bp.Book)
          .ToList();
      }

      return View(userBooks);
    }
    [Authorize(Roles = "Admin, User")]
    public ActionResult Details(int id)
    {
      Book thisBook = _db.Books
        .Include(book => book.JoinAuthorBook)
        .ThenInclude(join => join.Author)
        .Include(book => book.JoinBookCopy)
        .ThenInclude(join => join.Copy)
        .FirstOrDefault(book => book.BookId == id);
      return View(thisBook);
    }
    [Authorize(Roles = "Admin")]
    public ActionResult Create()
    {
      return View();
    }
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult> Create(Book book)
    {
      string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
      book.User = currentUser;

      _db.Books.Add(book);
      _db.SaveChanges();

      return RedirectToAction("Index");
      
    }
    [Authorize(Roles = "Admin")]
    public ActionResult AddAuthor(int id)
    {
      Book thisBook = _db.Books.FirstOrDefault(book => book.BookId == id);
      ViewBag.AuthorId = new SelectList(_db.Authors, "AuthorId", "AName");
      return View(thisBook);
    }
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public ActionResult AddAuthor(Book book, int authorId)
    {
      #nullable enable
      AuthorBook? joinEntity = _db.AuthorBooks.FirstOrDefault(join => join.AuthorId == authorId && join.BookId == book.BookId);
      #nullable disable
      if (joinEntity == null && authorId != 0)
      {
        _db.AuthorBooks.Add(new AuthorBook() { AuthorId = authorId, BookId = book.BookId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = book.BookId });
    }
    [Authorize(Roles = "Admin")]
    public ActionResult Edit(int id)
    {
      Book thisBook = _db.Books.FirstOrDefault(book => book.BookId == id);
      return View(thisBook);
    }
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public ActionResult Edit(Book book)
    {
      _db.Books.Update(book);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    [Authorize(Roles = "Admin")]
    public ActionResult Delete(int id)
    {
      Book thisBook = _db.Books.FirstOrDefault(book => book.BookId == id);
      return View(thisBook);
    }
    [Authorize(Roles = "Admin")]
    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Book thisBook = _db.Books.FirstOrDefault(book => book.BookId == id);
      _db.Books.Remove(thisBook);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public ActionResult DeleteAuthor(int joinId)
    {
      AuthorBook joinEntry = _db.AuthorBooks.FirstOrDefault(entry => entry.AuthorBookId == joinId);
      _db.AuthorBooks.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    [Authorize(Roles = "Admin")]
    public ActionResult AddCopy(int id)
    {
      Book thisBook = _db.Books.FirstOrDefault(book => book.BookId == id);
      return View(thisBook);
    }
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public ActionResult AddCopy(Copy copy, int bookId)
    {
      Book thisBook = _db.Books.FirstOrDefault(book => book.BookId == bookId);
      Copy newCopy = new Copy();
      newCopy.BookId = thisBook.BookId;

      _db.Copies.Add(newCopy);
      _db.SaveChanges();

      return RedirectToAction("Details", new { id = thisBook.BookId });
    }
    [Authorize(Roles = "Admin")]
    public ActionResult DeleteCopy(int id)
    {
      Copy thisCopy = _db.Copies.FirstOrDefault(copy => copy.CopyId == id);
      return View(thisCopy);
    }

    [HttpPost, ActionName("DeleteCopy")]
    public ActionResult DeleteCopyConfirmed(int id)
    {
      Copy thisCopy = _db.Copies.FirstOrDefault(copy => copy.CopyId == id);
      _db.Copies.Remove(thisCopy);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public ActionResult AddPatron(int id)
    {
      Copy thisCopy = _db.Copies.FirstOrDefault(copy => copy.CopyId == id);
      ViewBag.PatronId = new SelectList(_db.Patrons, "PatronId", "PName");
      return View(thisCopy);
    }
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public ActionResult AddPatron(Copy copy, int patronId)
    {
      #nullable enable
      BookPatron? joinEntity = _db.BookPatrons.FirstOrDefault(join => join.PatronId == patronId && join.CopyId == copy.CopyId);
      #nullable disable

      if (joinEntity == null && patronId > 0)
      {
        _db.BookPatrons.Add(new BookPatron() { PatronId = patronId, CopyId = copy.CopyId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = copy.BookId });
    }


  }  
}