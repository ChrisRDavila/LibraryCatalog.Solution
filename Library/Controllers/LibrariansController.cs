using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Library.Models;
using System.Collections.Generic;
using System.Linq;

namespace Library.Controllers
{
  public class LibrariansController : Controller
  {
    public readonly LibraryContext _db;

    public LibrariansController(LibraryContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      return View(_db.Librarians.ToList());
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Librarian librarian)
    {
      _db.Librarians.Add(librarian);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Librarian thisLibrarian = _db.Librarians.FirstOrDefault(librarian => librarian.LibrarianId == id);
      return View(thisLibrarian);
    }

    public ActionResult Edit(int id)
    {
      Librarian thisLibrarian = _db.Librarians.FirstOrDefault(librarian => librarian.LibrarianId == id);
      return View(thisLibrarian);
    }

    [HttpPost]
    public ActionResult Edit(Librarian librarian)
    {
      _db.Entry(librarian).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Librarian thisLibrarian = _db.Librarians.FirstOrDefault(librarian => librarian.LibrarianId == id);
      return View(thisLibrarian);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Librarian thisLibrarian = _db.Librarians.FirstOrDefault(librarian => librarian.LibrarianId == id);
      _db.Librarians.Remove(thisLibrarian);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}