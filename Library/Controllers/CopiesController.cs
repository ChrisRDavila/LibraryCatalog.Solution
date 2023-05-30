using Microsoft.AspNetCore.Mvc;
using Library.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace Library.Controllers
{
  public class CopiesController : Controller
  {
    private readonly LibraryContext _db;

    public CopiesController(LibraryContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      return View(_db.Copies.ToList());
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Copy copy)
    {
      _db.Copies.Add(copy);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Copy thisCopy = _db.Copies.FirstOrDefault(copies => copies.CopyId == id);
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
