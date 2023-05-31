using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Library.Models
{
  public class LibraryContext : IdentityDbContext<ApplicationUser>
  {
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Patron> Patrons { get; set; }
    public DbSet<Copy> Copies { get; set; }
    public DbSet<Checkout> Checkouts { get; set; }
    public DbSet<Librarian> Librarians { get; set; }
    //join table dbsets
    public DbSet<AuthorBook> AuthorBooks { get; set; }
    public DbSet<BookCopy> BookCopies { get; set; }
    public DbSet<BookCheckout> BookCheckouts { get; set; }
    public DbSet<BookPatron> BookPatrons { get; set; }
    public DbSet<LibrarianCopy> LibrarianCopies { get; set; }
    public DbSet<PatronCheckout> PatronCheckouts { get; set; }
    public LibraryContext(DbContextOptions options) : base(options) { }
  }
}