using Microsoft.AspNetCore.Mvc;
using Library.Models;
using System.Collections.Generic;
using System.Linq;


namespace Library.Controllers
{
  public class HomeController : Controller
  {
    private readonly LibraryContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public HomeController(UserManager<ApplicationUser> userManager, LibraryContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    [HttpGet("/")]
    public Task<ActionResult> Index()
    {
      Author[] authors = _db.Authors.ToArray();
      Book[] books = _db.Books.ToArray();
      Dictionary<string, object> model = new Dictionary<string, object>();
      model.Add("authors", authors);
      model.Add("books", books);
      return View(model);
    }
  }
}