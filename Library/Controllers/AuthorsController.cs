using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Library.Models;
using System.Collections.Generic;
using System.Linq;

public class AuthorsController : Controller
{
  private readonly LibraryContext _db;
  
  public AuthorsController(LibraryContext db)
  {
    _db = db;
  }

  public ActionResult Index()
  {
    List<Author> model = _db.Authors
                            .Include(author => author.Books)
                            .ToList();
    return View(model);
  }

  public ActionResult Create()
  {
    ViewBag.BookId = new SelectList(_db.Books, "BookId", "Title");
    return View();
  }

  [HttpPost]
  public ActionResult Create(Author author)
  {
    if (!ModelState.IsValid)
    {
      ViewBag.BookId = new SelectList(_db.Books, "BookId", "Title");
      return View(author);
    }
    else
    {
      _db.Authors.Add(author);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }

  public ActionResult Details(int id)
  {
    Author thisAuthor = _db.Authors
                            .Include(author => author.Books)
                            .Include(author => author.JoinEntities)
                            .ThenInclude(join => join.Book)
                            .FirstOrDefault(author => author.AuthorId == id);
    return View(thisAuthor);
  }

  public ActionResult Edit(int id)
  {
    Author thisAuthor = _db.Authors.FirstOrDefault(author => author.AuthorId == id);
    ViewBag.BookId = new SelectList(_db.Books, "BookId", "Title");
    return View(thisAuthor);
  }

  [HttpPost]
  public ActionResult Edit(Author author)
  {
    _db.AuthorBooks.Update(author);
    _db.SaveChanges();
    return RedirectToAction("Index");
  }

  public ActionResult Delete(int id)
  {
    Author thisAuthor = _db.Authors.FirstOrDefault(author => author.AuthorId == id);
    return View(thisAuthor);
  }

  [HttpPost, ActionName("Delete")]
  public ActionResult DeleteConfirmed(int id)
  {
    Author thisAuthor = _db.Authors.FirstOrDefault(author => author.AuthorId == id);
    _db.Authors.Remove(thisAuthor);
    _db.SaveChanges();
    return RedirectToAction("Index");
  }

  public ActionResult AddBook(int id)
  {
    Author thisAuthor = _db.Authors.FirstOrDefault(author => author.AuthorId == id);
    ViewBag.BookId = new SelectList(_db.Books, "BookId", "Title");
    return View(thisAuthor);
  }

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

  [HttpPost]
  public ActionResult DeleteJo
}