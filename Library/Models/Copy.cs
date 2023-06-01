using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Library.Models
{
  public class Copy
  {
    public int CopyId { get; set; }
    public int BookId { get; set; }
    [Required(ErrorMessage = "The copies serial id can't be empty!")]
    public string Serial { get; set; }
    [Required(ErrorMessage = "The copies quantity can't be empty!")]
    public int Quantity { get; set; }
    public Book Book { get; set; }
    public List<BookCopy> JoinBookCopy { get; }
    public List<LibrarianCopy> JoinLibrarianCopy { get; }
    public ApplicationUser User { get; set; }
  }
}